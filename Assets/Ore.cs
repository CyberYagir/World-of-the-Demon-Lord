using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public Item resource;
    public int hpOre = 8;
    PlayerController pl;
    public float minDist;
    float time, time2;
    private void Start()
    {
        pl = FindObjectOfType<PlayerController>();
    }

    public void OnMouseDown()
    {
        pl.point = transform.position;
    }


    private void Update()
    {
        time2 += Time.deltaTime;
        if (time2 > 30f)
        {
            if (hpOre < 8)
            {
                hpOre++;
                time2 = 0;
            }
        }
        GetComponent<MeshRenderer>().materials[0].SetFloat("_EmissiveExposureWeight", (hpOre/8f) * 0.825f);
        if (Vector3.Distance(transform.position, pl.transform.position) <= minDist)
        {
            if (pl.point == transform.position)
            {
                time += Time.deltaTime;
                pl.animator.Play("Kailo");
                if (time > 10f)
                {
                    if (hpOre != 0)
                    {
                        time2 = 0;
                        PlayerEquipent.InventoryAdd(resource.CloneItem());
                        hpOre -= 1;
                    }
                    time = 0;
                }
            }
            else
            {
                time = 0;
            }
        }
    }
}
