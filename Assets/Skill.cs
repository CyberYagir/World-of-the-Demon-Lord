using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Game/Buff", order = 1)]
public class Buff : ScriptableObject
{
    public float regenHp, regenMana;
    public float bonusHp, bonusMana;
    public float bonusDamage;
    public Sprite sprite;
    public float time;
}

[CreateAssetMenu(fileName = "Skill", menuName = "Game/Skill", order = 1)]
public class Skill : ScriptableObject
{
    public float maxAttackDistace;
    public float minAttackDistace;
    public float damage;
    public float skillTime;
    public float coolDown;
    public float mana;
    public float hp;

    [Header("Mage or Bow")]
    public bool isSpawn;
    public GameObject spawnObject;
    public string invoke;

    public enum skillType {selected, range};
    public skillType type;
    [Header("if range")]
    public float radius;

    public Sprite icon;
    public AnimationClip[] clips;
    public Buff[] buffs;
}

[System.Serializable]
public class SkillUse
{
    public string name;
    public Skill skill;
    public bool used;
    public float time,cooldown;
    public int num;
    public SkillUse(Skill skill)
    {
        this.skill = skill;
        name = skill.name;
        used = false;
        
        time = 0;
    }
}



