using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Image image;
    public TMP_Text _name, _descr;
    public Item item;

    public GameObject use, eqp, drp;

    public void Init(Item it)
    {
        item = it;
        image.sprite = item.sprite;
        _name.text = item.name;
        if (item.type == Item.itemType.dopWeapon || item.type == Item.itemType.head || item.type == Item.itemType.body || item.type == Item.itemType.legs)
        {
            _descr.text = "Rarity: " + item.rarity + "\nHp bonus: " + item.hpAdd * item.rarity + "\nMana bonus: " + item.manaAdd * item.rarity;
        }else if (item.type == Item.itemType.weapon)
        {
            _descr.text = "Rarity: " + item.rarity + "\nSkills:\n ";
            for (int i = 0; i < item.skills.Length; i++)
            {
                _descr.text += " Skill " + i + "\n\tDamage: " + item.skills[i].damage * item.rarity + "\n\t" +
                    "Cooldown: " + item.skills[i].coolDown + "\n\tMana: " + -(item.skills[i].mana) + "\n\tHp: " + -(item.skills[i].hp) + "\n\tSkill time: " + item.skills[i].skillTime + "\n";
            }

        }
        use.SetActive(item.type == Item.itemType.useble);
        eqp.SetActive(item.type != Item.itemType.resource && item.type != Item.itemType.useble);

    }

    public void Use()
    {
        ItemInvoke.Invoke(item);
        PlayerEquipent.InventoryRemove(item);
    }
    public void Equip()
    {
        if (item.type == Item.itemType.head)
        {
            if (PlayerEquipent.eq._hat == null)
            {
                PlayerEquipent.eq._hat = item;
                PlayerEquipent.InventoryRemove(item, -1);
            }
            else
            {
                PlayerEquipent.InventoryAdd(PlayerEquipent.eq._hat);
                PlayerEquipent.InventoryRemove(item, -1);
                PlayerEquipent.eq._hat = item;

            }
            var n = PlayerEquipent.eq.hat;
            if (n != null)
            {
                n.GetComponent<MeshFilter>().sharedMesh = item.globalPrefab.GetComponent<MeshFilter>().sharedMesh;
                n.GetComponent<Renderer>().sharedMaterials = item.globalPrefab.GetComponent<Renderer>().sharedMaterials;
            }
        }
        else if (item.type == Item.itemType.body)
        {
            if (PlayerEquipent.eq._body == null)
            {
                PlayerEquipent.eq._body = item;
                PlayerEquipent.InventoryRemove(item, -1);
            }
            else
            {
                PlayerEquipent.InventoryAdd(PlayerEquipent.eq._body);
                PlayerEquipent.InventoryRemove(item, -1);
                PlayerEquipent.eq._body = item;
            }

            var n = PlayerEquipent.eq.torso;
            if (n != null)
            {
                n.GetComponent<MeshFilter>().sharedMesh = item.wearMesh;

                n.GetComponent<MeshRenderer>().materials[0].SetColor("_BaseColor", item.wearColor);
                if (PlayerEquipent.eq.torso.GetComponent<Renderer>().materials.Length == 2)
                {
                    PlayerEquipent.eq.torso.GetComponent<Renderer>().materials[1].SetColor("_BaseColor", item.wearColor);
                }
                PlayerEquipent.eq.leftSh.GetComponent<MeshFilter>().sharedMesh = item.wearMesh2;
                PlayerEquipent.eq.rightSh.GetComponent<MeshFilter>().sharedMesh = item.wearMesh2;

                PlayerEquipent.eq.leftSh.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", item.wearColor);
                PlayerEquipent.eq.rightSh.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", item.wearColor);
            }
        }
        else if (item.type == Item.itemType.legs)
        {
            if (PlayerEquipent.eq._legs == null)
            {
                PlayerEquipent.eq._legs = item;
                PlayerEquipent.InventoryRemove(item, -1);
            }
            else
            {
                PlayerEquipent.InventoryAdd(PlayerEquipent.eq._legs);
                PlayerEquipent.InventoryRemove(item, -1);
                PlayerEquipent.eq._legs = item;
            }

            for (int i = 0; i <PlayerEquipent.eq.legLeft.Length; i++)
            {
                PlayerEquipent.eq.legLeft[i].GetComponent<Renderer>().material.SetColor("_BaseColor", item.wearColor);
            }
            for (int i = 0; i < PlayerEquipent.eq.legRight.Length; i++)
            {
                PlayerEquipent.eq.legRight[i].GetComponent<Renderer>().material.SetColor("_BaseColor", item.wearColor);
            }

        } //123
        else if (item.type == Item.itemType.weapon)
        {
            if (PlayerEquipent.eq._weapon == null)
            {
                PlayerEquipent.eq._weapon = item;
                PlayerEquipent.InventoryRemove(item, -1);
            }
            else
            {
                PlayerEquipent.InventoryAdd(PlayerEquipent.eq._weapon);
                PlayerEquipent.InventoryRemove(item, -1);
                PlayerEquipent.eq._weapon = item;
            }
        }
        else if (item.type == Item.itemType.dopWeapon)
        {
            if (PlayerEquipent.eq._dopWeapon == null)
            {
                PlayerEquipent.eq._dopWeapon = item;
                PlayerEquipent.InventoryRemove(item, -1);
            }
            else
            {
                PlayerEquipent.InventoryAdd(PlayerEquipent.eq._dopWeapon);
                PlayerEquipent.InventoryRemove(item, -1);
                PlayerEquipent.eq._dopWeapon = item;
            }
        }
    }
    public void Drop()
    {
        var it = item.CloneItem();
        it.value = 1;
        Instantiate(PlayerEquipent.eq.drop.gameObject, PlayerEquipent.eq.dropPoint.transform.position, Quaternion.identity).GetComponent<Drop>().item = it;
        PlayerEquipent.InventoryRemove(item, 1);
    }
}
