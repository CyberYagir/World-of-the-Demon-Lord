using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mob;
    public List<GameObject> gameObjects;
    public int count;
    public float radius;
    private void Start()
    {
        StartCoroutine(loop());
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(radius * 2, 5f, radius * 2));
    }
    IEnumerator loop()
    {
        while (true)
        {
            while (gameObjects.FindAll(x=>x == null).Count != 0)
            {
                print("Text");
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i] == null) { gameObjects.RemoveAt(i); break; };
                }
            }
            while (gameObjects.Count < count)
            {
                yield return new WaitForSeconds(1f);
                RaycastHit hit;
                var pos = transform.position + new Vector3(Random.Range(-radius, radius), 50f, Random.Range(-radius, radius));
                if (Physics.Raycast(pos, Vector3.down, out hit))
                {
                    if (hit.transform.tag == "CanWalk")
                    {
                        gameObjects.Add(Instantiate(mob.gameObject, hit.point, Quaternion.identity, transform));
                    }
                }                
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
