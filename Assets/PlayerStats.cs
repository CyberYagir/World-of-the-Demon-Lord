using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health, mana;
    public float manaAdd, dopManaAdd;
    public float healthMax, manaMax;
    public float dopHealth, dopMana;
    public int exp, endExp, lvl;
    public float cooldownMin = 1;
    public float damagePercents = 1f;
    public float speedPercents = 1f, manaPercents = 1f;
    public int skillPoints;
    public List<SkillUse> skillsPoses;
    public static PlayerStats stats;
    public bool dead;
    public float deadTime;
    private void Start()
    {
        stats = this;
    }

    private void Update()
    {
        if (dead)
        {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<Animator>().Play("Death");
            deadTime += Time.deltaTime;
            if (deadTime > 5f)
            {
                GetComponent<PlayerController>().enabled = true;
                GetComponent<PlayerController>().point = Vector3.zero;
                deadTime = 0;
                health = healthMax;
                mana = manaMax;
                dead = false;
                transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            }
            return;
        }



        dopHealth = (PlayerEquipent.eq._hat != null ? PlayerEquipent.eq._hat.hpAdd : 0) +
                    (PlayerEquipent.eq._body != null ? PlayerEquipent.eq._body.hpAdd : 0) +
                    (PlayerEquipent.eq._legs != null ? PlayerEquipent.eq._legs.hpAdd : 0) +
                    (PlayerEquipent.eq._weapon != null ? PlayerEquipent.eq._weapon.hpAdd : 0) +
                    (PlayerEquipent.eq._dopWeapon != null ? PlayerEquipent.eq._dopWeapon.hpAdd : 0);
        dopMana = (PlayerEquipent.eq._hat != null ? PlayerEquipent.eq._hat.manaAdd : 0) +
                    (PlayerEquipent.eq._body != null ? PlayerEquipent.eq._body.manaAdd : 0) +
                    (PlayerEquipent.eq._legs != null ? PlayerEquipent.eq._legs.manaAdd : 0) +
                    (PlayerEquipent.eq._weapon != null ? PlayerEquipent.eq._weapon.manaAdd : 0) +
                    (PlayerEquipent.eq._dopWeapon != null ? PlayerEquipent.eq._dopWeapon.manaAdd : 0);

        mana += (manaAdd + dopManaAdd) * Time.deltaTime * manaPercents * 2.5f;
        if (mana > manaMax + dopMana)
        {
            mana = manaMax + dopMana;
        }
        if (health > healthMax + dopHealth)
        {
            health = healthMax + dopHealth;
        }
        if (health <= 0) dead = true;
    }

    public void AddExp(int exAdd)
    {
        for (int i = 0; i < exAdd; i++)
        {
            exp++;
            if (exp > endExp)
            {
                lvl++;
                skillPoints++;
                endExp += (int)(endExp * (0.2f * lvl));
                exp = 0;
            }
        }
    }
}
