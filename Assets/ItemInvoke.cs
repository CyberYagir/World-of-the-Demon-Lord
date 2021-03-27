using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvoke : MonoBehaviour
{
    public Item item;
    public static ItemInvoke iti;

    private void Start()
    {
        iti = this;
    }

    public static void Invoke(Item it)
    {
        iti.item = it;
        iti.Invoke(iti.item.invokeMethod, 0);
    }

    public void AddHeath()
    {
        PlayerStats.stats.health += int.Parse(item.keys.Find(x => x.name == "Add").vel);
    }
    public void AddMana()
    {
        PlayerStats.stats.mana += int.Parse(item.keys.Find(x => x.name == "Add").vel);
    }
}
