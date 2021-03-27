using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loc6Manager : MonoBehaviour
{
    public GameObject globalLight;
    public GameObject loc6;

    private void Update()
    {
        globalLight.active = !loc6.active; 
    }
}
