using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDrop : MonoBehaviour
{
    public Item[] items;
    public bool drop;
    public void Drop()
    {
        if (!drop)
        {
            if (GetComponent<Mob>().player != null)
                if (GetComponent<Mob>().player.GetComponent<PlayerStats>() != null)
                    GetComponent<Mob>().player.GetComponent<PlayerStats>().AddExp(GetComponent<Mob>().exp);
            var id = Random.Range(0, items.Length);
            if (items[id] != null) {
                var it = items[id].CloneItem();
                if (it.type != Item.itemType.useble && it.type != Item.itemType.resource)
                {
                    it.manaAdd /= 2;
                    it.hpAdd /= 2;
                }
                Instantiate(PlayerEquipent.eq.drop.gameObject, transform.position + new Vector3(0, 2f, 0), Quaternion.identity).GetComponent<Drop>().item = it;
            }
            drop = true;
        }
    }
}
