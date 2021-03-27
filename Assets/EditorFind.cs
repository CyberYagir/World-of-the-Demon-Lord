using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class EditorFind : MonoBehaviour
{
    public GameObject[] objects;
    private void Update()
    {
        objects = GameObject.FindGameObjectsWithTag("EditorOnly");
    }
}
