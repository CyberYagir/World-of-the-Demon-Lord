using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        if (item != null)
        {
            Instantiate(item.globalPrefab, transform);
        }
    }
    private void Update()
    {
        if (transform.position.y < -15) Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerEquipent.InventoryAdd(item);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {

        }
    }
}
