using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFontan : MonoBehaviour
{
    public float time;
    public Collider collider;
    public ParticleSystem particleSystem;
    public bool triggered;


    private void Start()
    {
        Destroy(gameObject, 6f);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 3f)
        {
            particleSystem.emissionRate = 60;
            if (triggered)
            {
                PlayerStats.stats.health -= 5 * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
}
