using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlace : MonoBehaviour
{
    public Vector3 size, offcet;
    public GameObject[] trees;
    public int count;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            RaycastHit hit;
            Vector3 pos = new Vector3(Random.Range(-size.x, size.x), 0 , Random.Range(-size.z, size.z));
            if (Physics.Raycast(transform.position + pos, Vector3.down, out hit))
            {
                if (hit.transform.tag == "CanWalk")
                    Instantiate(trees[Random.Range(0, trees.Length)], hit.point, Quaternion.Euler(-90, 0, 0), transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + offcet, size);
    }
}
