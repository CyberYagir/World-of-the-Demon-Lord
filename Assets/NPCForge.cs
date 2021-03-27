using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CraftResItem
{
    public Item item;
    public int value;
}
[System.Serializable]
public class CraftItem
{
    public Item item;
    public List<CraftResItem> craftResItems;
    public int level;
    public int exp;
}
public class NPCForge : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public string shopName;
    public List<CraftItem> crafts = new List<CraftItem>();
    public Transform holder, item, resItem;
    float time = 0;

    private void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            canvas.SetActive(!canvas.active);
        }
    }

    private void Start()
    {
        crafts = FindObjectOfType<AllShops>().shops.Find(x => x.name == shopName).crafts;
        for (int i = 0; i < crafts.Count; i++)
        {
            var f = Instantiate(item, holder.transform);
            f.transform.GetChild(0).GetComponent<Image>().sprite = crafts[i].item.sprite;
            f.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = crafts[i].item.name + $" [{crafts[i].level}]\nEXP: " + crafts[i].exp;
            f.GetComponent<ForgeItem>().craftID = i;
            for (int j = 0; j < crafts[i].craftResItems.Count; j++)
            {
                var g = Instantiate(resItem, f.transform.GetChild(1).transform);
                f.transform.GetChild(1).GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
                g.GetComponent<Image>().sprite = crafts[i].craftResItems[j].item.sprite;
                g.GetChild(0).GetComponent<TMP_Text>().text = crafts[i].craftResItems[j].item.name + $" [{crafts[i].craftResItems[j].value}]";
                g.gameObject.SetActive(true);
            }
            f.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            f.gameObject.SetActive(true);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(holder.GetComponent<RectTransform>());

    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            canvas.SetActive(false);
        }
        else
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(holder.GetComponent<RectTransform>());
        }
    }
}
