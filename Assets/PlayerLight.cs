using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Transform point;
    public float speed;



    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, point.transform.position, speed * Time.deltaTime);
    }
}
