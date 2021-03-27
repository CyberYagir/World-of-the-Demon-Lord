using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmozIndic : MonoBehaviour
{
    public Color color;
    public float rad;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, rad);
    }
}
