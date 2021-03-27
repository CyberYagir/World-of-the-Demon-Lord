using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public LineRenderer lineRenderer;
    public float time = 0;
    private void Start()
    {
        transform.position += new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 4f), Random.Range(-3f, 3f));
        Destroy(gameObject,10f);
    }

    private void Update()
    {
        if (target == null)
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
            float dist = 99999999; ;
            int id = -1;
            var mobs = FindObjectsOfType<Mob>().ToList().FindAll(x=>x.hp > 0);
            for (int i = 0; i < mobs.Count; i++)
            {
                var d = Vector3.Distance(transform.position, mobs[i].transform.position);
                if (d < dist)
                {
                    dist = d;
                    id = i;
                }
            }
            if (id != -1 && dist < 10f)
            {
                target = mobs[id].gameObject;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, GameObject.FindGameObjectWithTag("Point").transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                if (target != null)
                {
                    if (target.GetComponent<Mob>() != null)
                    {
                        target.GetComponent<Mob>().hp -= (float)GetComponent<SpawnebleItem>().keys[1];
                        target.GetComponent<Mob>().triggered = true;
                        if (target.GetComponent<Mob>().hp < 0)
                        {
                            target = null;
                        }
                    }
                    else
                    {
                        target = null;
                    }
                }
                time = 0;
            }
        }
    }
}
