using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
[System.Serializable]
public class Key {
    public string name;
    public string vel;
}


[CreateAssetMenu(fileName = "Item", menuName = "Game/Item", order = 1)]
public class Item : ScriptableObject
{
    public enum itemType {head, body, legs, weapon, dopWeapon, resource, useble};
    public itemType type;
    public Sprite sprite;
    public GameObject globalPrefab;
    public int rarity = 1;
    public bool canStack;
    public int value = 1;

    [Header("Head, Body, Legs")]
    public Mesh wearMesh;
    public Mesh wearMesh2;
    public Color wearColor, wearColor2;
    public float manaAdd, hpAdd;




    [Header("Weapon, dopWeapon")]
    public GameObject weaponPrefab;
    public Skill[] skills;

    [Header("Usable")]
    public string invokeMethod;
    public List<Key> keys;

    public Item(int rar, Skill[] skills)
    {
        this.skills = (Skill[])skills.Clone();
        this.rarity = rar;
        for (int i = 0; i < this.skills.Length; i++)
        {
            this.skills[i].damage *= 1 + ((float)rar / 10f);
        }
    }

    public Item CloneItem()
    {
        return new Item(rarity, skills) { name = name, globalPrefab = globalPrefab, skills = skills, sprite = sprite, type = type, weaponPrefab = weaponPrefab, wearColor = wearColor, wearMesh = wearMesh, canStack = canStack, hpAdd = hpAdd, invokeMethod = invokeMethod, keys = keys, wearMesh2 = wearMesh2, manaAdd = manaAdd, rarity = rarity, value = value };
    }
}
