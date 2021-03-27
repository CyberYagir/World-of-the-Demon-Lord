using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public Item.itemType itemType;



    private void Update()
    {
        if (itemType == Item.itemType.head)
            image.sprite = PlayerEquipent.eq._hat != null ? PlayerEquipent.eq._hat.sprite : null;
        if (itemType == Item.itemType.body)
            image.sprite = PlayerEquipent.eq._body != null ? PlayerEquipent.eq._body.sprite : null;
        if (itemType == Item.itemType.legs)
            image.sprite = PlayerEquipent.eq._legs != null ? PlayerEquipent.eq._legs.sprite : null;
        if (itemType == Item.itemType.weapon)
            image.sprite = PlayerEquipent.eq._weapon != null ? PlayerEquipent.eq._weapon.sprite : null;
        if (itemType == Item.itemType.dopWeapon)
            image.sprite = PlayerEquipent.eq._dopWeapon != null ? PlayerEquipent.eq._dopWeapon.sprite : null;
        if (image.sprite == null)
        {
            image.color = new Color(0, 0, 0, 0);
        }
        else
            image.color = new Color(1, 1, 1, 1);
    }

    public void UnEquip()
    {
        Item it;

        switch (itemType)
        {
            case Item.itemType.head:
                it = PlayerEquipent.eq._hat;
                break;
            case Item.itemType.body:
                it = PlayerEquipent.eq._body;
                break;
            case Item.itemType.legs:
                it = PlayerEquipent.eq._legs;
                break;
            case Item.itemType.weapon:
                it = PlayerEquipent.eq._weapon;
                break;
            case Item.itemType.dopWeapon:
                it = PlayerEquipent.eq._dopWeapon;
                break;
            default:
                it = new Item(0, new Skill[0]) { name = "Error"};
                break;
        }

        PlayerEquipent.InventoryAdd(it);

        switch (itemType)
        {
            case Item.itemType.head:
                PlayerEquipent.eq._hat = null;
                PlayerEquipent.eq.hat.GetComponent<MeshFilter>().sharedMesh = null;
                break;
            case Item.itemType.body:
                PlayerEquipent.eq._body = null;
                PlayerEquipent.eq.torso.GetComponent<MeshFilter>().sharedMesh =  PlayerEquipent.eq.standardBody;
                PlayerEquipent.eq.torso.GetComponent<Renderer>().materials[0] = PlayerEquipent.eq.skin;
                if (PlayerEquipent.eq.torso.GetComponent<Renderer>().materials.Length == 2)
                {
                    PlayerEquipent.eq.torso.GetComponent<Renderer>().materials[1] = PlayerEquipent.eq.skin;
                }
                PlayerEquipent.eq.leftSh.GetComponent<MeshFilter>().sharedMesh = PlayerEquipent.eq.standardSholders;
                PlayerEquipent.eq.rightSh.GetComponent<MeshFilter>().sharedMesh = PlayerEquipent.eq.standardSholders;
                break;
            case Item.itemType.legs:
                PlayerEquipent.eq._legs = null;


                for (int i = 0; i < PlayerEquipent.eq.legLeft.Length; i++)
                {
                    PlayerEquipent.eq.legLeft[i].GetComponent<MeshRenderer>().material = PlayerEquipent.eq.skin;
                }
                for (int i = 0; i < PlayerEquipent.eq.legRight.Length; i++)
                {
                    PlayerEquipent.eq.legRight[i].GetComponent<MeshRenderer>().material = PlayerEquipent.eq.skin;
                }
                break;
            case Item.itemType.weapon:
                PlayerEquipent.eq._weapon = null;
                break;
            case Item.itemType.dopWeapon:
                PlayerEquipent.eq._dopWeapon = null;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UnEquip();
    }
}
