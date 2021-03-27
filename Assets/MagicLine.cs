using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLine : MonoBehaviour
{
    public Transform target;
    public float speed, time;
    public Vector3 pos;
    public LineRenderer lineRenderer;
    private void Start()
    {
        pos = transform.position + (new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)) * Random.Range(-8f, 8f));
        if (GetComponent<SpawnebleItem>().keys.Count != 0)
        {
            if (GetComponent<SpawnebleItem>().keys[0] == null)
            {
                GetComponent<SpawnebleItem>().keys = new List<object>();
                return;
            }
            target = (GetComponent<SpawnebleItem>().keys[0] as Mob).transform;
        }
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        if (other.transform.tag != "Player")
        {
            if (target != null && target == other.transform) return;
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (target != null)
        {
            lineRenderer.SetPosition(0, target.transform.position);
            lineRenderer.SetPosition(1, transform.position);

            if (GetComponent<SpawnebleItem>().keys.Count == 2)
            {
                (GetComponent<SpawnebleItem>().keys[0] as Mob).hp -= (float)GetComponent<SpawnebleItem>().keys[1];
                (GetComponent<SpawnebleItem>().keys[0] as Mob).triggered = true;
                Destroy(gameObject, 2);
            }

        }
        else
        {
            lineRenderer.SetPosition(0, pos);
            lineRenderer.SetPosition(1, transform.position);
        }

    }
}
