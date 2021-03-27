using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform invItem;
    public Transform invHolder;
    public GameObject invCamera, cameraMove;
    public void OpenInv()
    {
        cameraMove.SetActive(false);
        invCamera.SetActive(true);
    }
    public void CloseInv()
    {
        cameraMove.SetActive(true);
        invCamera.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInv();
        }
    }
    public void UpdateInventory()
    {
        foreach (Transform item in invHolder)
        {
            Destroy(item.gameObject);
        }
        invItem.gameObject.SetActive(true);
        for (int i = 0; i < PlayerEquipent.eq.inventory.Count; i++)
        {
            var g = Instantiate(invItem.gameObject, invHolder.transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = PlayerEquipent.eq.inventory[i].sprite;
            g.transform.GetChild(1).GetComponent<TMP_Text>().text = PlayerEquipent.eq.inventory[i].value == 1 ? "" : PlayerEquipent.eq.inventory[i].value.ToString();
            g.transform.GetComponent<InventoryItem>().item = PlayerEquipent.eq.inventory[i];
        }
        invItem.gameObject.SetActive(false);
    }
}
