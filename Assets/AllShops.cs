using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Shop{
    public string name;
    public List<CraftItem> crafts;
}
//[ExecuteInEditMode]
public class AllShops : MonoBehaviour
{
    public List<Shop> shops = new List<Shop>();
    public static AllShops allShops;

    private void Start()
    {
        allShops = this;
    }

    private void Update()
    {
    }
}
