using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target;
    public float speed, time;

    private void Start()
    {
        if (GetComponent<SpawnebleItem>().keys.Count != 0)
        {
            if (GetComponent<SpawnebleItem>().keys[0] == null)
            {
                GetComponent<SpawnebleItem>().keys = new List<object>();
                return;
            }
            target = (GetComponent<SpawnebleItem>().keys[0] as Mob).transform;
        }
        if (target != null)
        {
            transform.LookAt(target.transform.position);
        }
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
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 1, 0), speed * Time.deltaTime);
            if (Vector3.Distance(target.transform.position + new Vector3(0, 1, 0), transform.position) < 0.01f)
            {
                if (GetComponent<SpawnebleItem>().keys.Count == 2)
                {
                    (GetComponent<SpawnebleItem>().keys[0] as Mob).hp -= (float)GetComponent<SpawnebleItem>().keys[1];
                    (GetComponent<SpawnebleItem>().keys[0] as Mob).triggered = true;
                }
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.forward * 10000f, speed * Time.deltaTime);

        }
        time += Time.deltaTime;
        if (time > 30f)
        {
            Destroy(gameObject);
        }
    }
}
