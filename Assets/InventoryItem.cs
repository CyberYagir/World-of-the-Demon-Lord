using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerEquipent.eq.GetComponent<PlayerUI>().inventoryInfo.GetComponent<ItemInfo>().Init(item);
        PlayerEquipent.eq.GetComponent<PlayerUI>().inventoryInfo.SetActive(true);

    }
}
