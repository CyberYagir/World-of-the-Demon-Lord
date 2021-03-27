using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMove : MonoBehaviour
{
    public Vector3 offcet;
    public Transform player;
    public float speed;
    public GameObject[] locs;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < locs.Length; i++)
        //{
        //    locs[i].SetActive(false);
        //}
        //locs[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,  player.transform.position + offcet, speed * Time.deltaTime);
    }
}
