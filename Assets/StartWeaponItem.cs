using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartWeaponItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color noSelect;
    public Image image;
    public Item item;

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerEquipent.eq._weapon = item.CloneItem();
        GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = noSelect;
    }

    
}
