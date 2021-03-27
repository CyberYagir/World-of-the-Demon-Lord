using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeItem : MonoBehaviour
{
    public int craftID;



    public void CraftItem()
    {
        var craft = GetComponentInParent<NPCForge>().crafts[craftID];
        var inv = FindObjectOfType<PlayerEquipent>().inventory;
        if (PlayerStats.stats.exp < craft.exp) return;


        for (int i = 0; i < craft.craftResItems.Count; i++)
        {
            if (inv.Find(x => x.name == craft.craftResItems[i].item.name) == null) return;
            else
                if (inv.Find(x => x.name == craft.craftResItems[i].item.name).value < craft.craftResItems[i].value) return;
        }
        for (int i = 0; i < craft.craftResItems.Count; i++)
        {
            PlayerEquipent.InventoryRemove(inv.Find(x => x.name == craft.craftResItems[i].item.name), craft.craftResItems[i].value);
        }
        PlayerStats.stats.exp -= craft.exp;
        var it = craft.item.CloneItem();
        it.rarity = Random.Range(1, 6);
        PlayerEquipent.InventoryAdd(it);
    }
}
