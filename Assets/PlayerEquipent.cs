using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipent : MonoBehaviour
{
    public GameObject drop;
    public Mesh standardBody, standardSholders;
    public Material skin;

    public static PlayerEquipent eq;
    public Transform hand, hand2, torso;
    public Transform leftSh, rightSh;
    public Transform hat;
    public Transform underFace;
    public Transform[] legLeft, legRight;
    public Transform dropPoint;

    public Item _weapon, _dopWeapon;
    Item oldWeapon, oldDopWeapon;
    public Item _hat, _body, _legs;

    public List<Item> inventory = new List<Item>();



    public static void InventoryAdd(Item item)
    {
        if (item.canStack)
        {
            if (eq.inventory.Find(x => x.name == item.name))
            {
                eq.inventory.Find(x => x.name == item.name).value++;
            }
            else
            {
                eq.inventory.Add(item.CloneItem());
            }
        }
        else
        {
            eq.inventory.Add(item.CloneItem());
        }
        eq.GetComponent<PlayerUI>().inventoryUI.UpdateInventory();
    }

    public static bool InventoryRemove(Item item, int count = 1)
    {
        bool end = false;
        var find = eq.inventory.Find(x => x.name == item.name);
        if (find != null)
        {
            if (count != -1)
            {
                find.value -= count;
                if (find.value <= 0)
                {
                    eq.inventory.Remove(find);
                    end = true;
                }
            }
            else
            {
                end = true;
                eq.inventory.Remove(find);
            }
        }
        eq.GetComponent<PlayerUI>().inventoryInfo.SetActive(false);
        eq.GetComponent<PlayerUI>().inventoryUI.UpdateInventory();
        return end;
    }



    private void Start()
    {
        eq = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].name == "HP Potion")
                {
                    ItemInvoke.Invoke(inventory[i]);
                    PlayerEquipent.InventoryRemove(inventory[i]);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].name == "Mana Potion")
                {
                    ItemInvoke.Invoke(inventory[i]);
                    PlayerEquipent.InventoryRemove(inventory[i]);
                    break;
                }
            }
        }


        if (oldWeapon != _weapon)
        {
            if (_weapon != null)
            {
                if (oldWeapon != null)
                {
                    var stats = PlayerStats.stats;
                    stats.skillsPoses = new List<SkillUse>();
                }
                for (int i = 0; i < _weapon.skills.Length; i++)
                {
                    PlayerStats.stats.skillsPoses.Add(new SkillUse(_weapon.skills[i]) { num = i+1});
                }
                if (hand.childCount != 0)
                {
                    Destroy(hand.GetChild(0).gameObject);
                }
                FindObjectOfType<PlayerUI>().UpdateSkills();
                var w = Instantiate(_weapon.weaponPrefab.gameObject, hand.transform);
                w.transform.SetAsFirstSibling();
                w.transform.localPosition = Vector3.zero;
                w.transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                if (hand.childCount == 1)
                {
                    Destroy(hand.GetChild(0).gameObject);
                    var stats = PlayerStats.stats;
                    stats.skillsPoses = new List<SkillUse>();
                    FindObjectOfType<PlayerUI>().UpdateSkills();
                }
            }
            oldWeapon = _weapon;
        }


        if (oldDopWeapon != _dopWeapon)
        {
            if (_dopWeapon != null)
            {
                if (hand2.childCount != 0)
                {
                    Destroy(hand2.GetChild(0).gameObject);
                }
                var w = Instantiate(_dopWeapon.globalPrefab.gameObject, hand2.transform);
                w.transform.SetAsFirstSibling();
                w.transform.localPosition = Vector3.zero;
                w.transform.localEulerAngles = Vector3.zero;
            }
            else
            {
                if (hand2.childCount == 1)
                {
                    Destroy(hand2.GetChild(0).gameObject);
                }
            }
            oldDopWeapon = _dopWeapon;
        }
    }
}
