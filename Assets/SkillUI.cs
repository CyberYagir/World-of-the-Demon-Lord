using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public int skillUseID;
    public Image image;
    public RectTransform cooldown, used;
    public SkillUse skillUse;


    private void Start()
    {
        skillUse = PlayerStats.stats.skillsPoses.Find(x => x.num == skillUseID);
        image.sprite = skillUse.skill.icon;
    }

    public void Update()
    {
        used.localScale = new Vector3(1, (skillUse.time > 0 ? skillUse.time : 0) / skillUse.skill.skillTime, 1);
        cooldown.localScale = new Vector3(1, (skillUse.cooldown > 0 ? skillUse.cooldown : 0) / skillUse.skill.coolDown, 1);

    }
}
