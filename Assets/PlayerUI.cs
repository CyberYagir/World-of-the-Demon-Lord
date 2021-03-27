using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public RectTransform hp, mana, exp;
    public List<SkillUI> skills;
    public Transform holder, item;
    public TMP_Text lvl;
    public InventoryUI inventoryUI;
    public GameObject inventoryInfo;

    private void Update()
    {
        var n = PlayerStats.stats;
        hp.localScale = new Vector3(1, n.health/(n.healthMax + n.dopHealth), 1);
        mana.localScale = new Vector3(1, n.mana/(n.manaMax + n.dopMana), 1);
        exp.localScale = new Vector3((float)n.exp / (float)n.endExp, 1, 1);
        lvl.text = n.lvl + "";
    }

    public void UpdateSkills()
    {
        foreach (var item in skills)
        {
            Destroy(item.gameObject);
        }
        skills = new List<SkillUI>();
        var stats = PlayerStats.stats.skillsPoses.OrderBy(x => x.num).ToList();
        for (int i = 0; i < stats.Count; i++)
        {
            var g = Instantiate(item.gameObject, holder.transform);
            skills.Add(g.GetComponent<SkillUI>());
            g.GetComponent<SkillUI>().skillUseID = stats[i].num;
            g.SetActive(true);
        }
    }
}
