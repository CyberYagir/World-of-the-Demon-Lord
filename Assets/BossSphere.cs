using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSphere : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        Destroy(gameObject, 20f);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            print("Pizdec");
            PlayerStats.stats.health -= 20f;
        }
    }
}
