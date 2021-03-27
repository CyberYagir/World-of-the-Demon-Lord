using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvoke : MonoBehaviour
{
    public Skill skill;
    public static SkillInvoke singelton;

    private void Start()
    {
        singelton = this;
    }

    public static void InvokeV(Skill sc)
    {
        singelton.skill = sc;
        if (sc.invoke != "")
            singelton.Invoke(sc.invoke, 0);
    }
    public void SpawnSummons()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(skill.spawnObject, PlayerEquipent.eq.hand.transform.position, Quaternion.identity).GetComponent<SpawnebleItem>().keys.AddRange(new List<object>() {null, skill.damage * PlayerStats.stats.damagePercents });
        }
    }

    public void CrossBowSkill2AnimatorEvent()
    {
        var transform = PlayerEquipent.eq.hand.transform;
        var mobs = FindObjectsOfType<Mob>();
        List<Mob> mobsCan = new List<Mob>();
        for (int h = 0; h < mobs.Length; h++)
        {
            var dst = Vector3.Distance(transform.position, mobs[h].transform.position);
            if (dst < skill.maxAttackDistace)
            {
                mobsCan.Add(mobs[h]);
            }
        }
        for (int g = 0; g < mobsCan.Count; g++)
        {
            Instantiate(skill.spawnObject, PlayerEquipent.eq.hand.transform.position, Quaternion.identity).GetComponent<SpawnebleItem>().keys.AddRange(new List<object>() { mobsCan[g], skill.damage * PlayerStats.stats.damagePercents });
        }
    }

    public void BowSkill2()
    {
        var transform = PlayerEquipent.eq.hand.transform;
        var mobs = FindObjectsOfType<Mob>();
        List<Mob> mobsCan = new List<Mob>();
        for (int h = 0; h < mobs.Length; h++)
        {
            var dst = Vector3.Distance(transform.position, mobs[h].transform.position);
            if (dst < skill.maxAttackDistace)
            {
                mobsCan.Add(mobs[h]);
            }
        }
        for (int g = 0; g < mobsCan.Count; g++)
        {
            Instantiate(skill.spawnObject, PlayerEquipent.eq.hand.transform.position, Quaternion.identity).GetComponent<SpawnebleItem>().keys.AddRange(new List<object>() { mobsCan[g], skill.damage * PlayerStats.stats.damagePercents });
        }
    }
}
