using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Mob selectedMob;
    public Animator animator;
    public bool play, attaked;
    int currClip;
    public void Attack()
    {
        GetComponent<PlayerController>().point = selectedMob.transform.position;
    }

    private void Update()
    {
        if (selectedMob != null)
        {
            transform.LookAt(new Vector3(selectedMob.transform.position.x, transform.position.y, selectedMob.transform.position.z));
        }
        GetComponent<PlayerController>().enabled = !play;
        var st = GetComponent<PlayerStats>();
        foreach (var it in st.skillsPoses)
        {
            it.time -= Time.deltaTime;
            if (it.time <= 0)
            {
                it.cooldown -= Time.deltaTime * st.cooldownMin;
            }
            if (it.used)
            {
                if (!attaked)
                {
                    if (it.time < it.skill.skillTime / 2)
                    {
                        if (it.skill.type == Skill.skillType.selected)
                        {
                            if (selectedMob != null)
                            {
                                var dist = Vector3.Distance(selectedMob.transform.position, transform.position);
                                if (dist >= it.skill.minAttackDistace && dist <= it.skill.maxAttackDistace)
                                {
                                    if (!it.skill.isSpawn)
                                    {
                                        selectedMob.hp -= it.skill.damage;
                                        if (selectedMob.hp <= 0)
                                        {
                                            selectedMob.select.SetActive(false);
                                            selectedMob = null;
                                        }
                                    }
                                    else
                                    {
                                        Instantiate(it.skill.spawnObject, transform.position, Quaternion.identity).GetComponent<SpawnebleItem>().keys.Add(selectedMob);
                                    }
                                }
                            }
                            else
                            {
                                var mobs = FindObjectsOfType<Mob>();
                                float min_dist = 999999999;
                                int id = -1;
                                for (int h = 0; h < mobs.Length; h++)
                                {
                                    var dst = Vector3.Distance(transform.position, mobs[h].transform.position);
                                    if (dst < min_dist)
                                    {
                                        min_dist = dst;
                                        id = h;
                                    }
                                }
                                if (id != -1)
                                {
                                    if (min_dist >= it.skill.minAttackDistace && min_dist <= it.skill.maxAttackDistace)
                                    {
                                        transform.LookAt(new Vector3(mobs[id].transform.position.x, transform.position.y, mobs[id].transform.position.z));
                                        var mob = mobs[id].GetComponent<Mob>();
                                        if (!it.skill.isSpawn)
                                        {
                                            mob.hp -= it.skill.damage;
                                            if (mob.hp <= 0)
                                            {
                                                mob.select.SetActive(false);
                                            }
                                        }
                                        else
                                        {
                                            Instantiate(it.skill.spawnObject, PlayerEquipent.eq.hand.transform.position, Quaternion.identity).GetComponent<SpawnebleItem>().keys.AddRange(new List<object>() { mob, it.skill.damage * st.damagePercents });
                                        }
                                    }
                                    else
                                    {
                                        if (it.skill.isSpawn)
                                        {
                                            var n = Instantiate(it.skill.spawnObject, PlayerEquipent.eq.hand.transform.position, Quaternion.identity);
                                            n.transform.localEulerAngles = transform.localEulerAngles;
                                            n.GetComponent<SpawnebleItem>().keys.AddRange(new List<object>() { null, it.skill.damage * st.damagePercents }); ;
                                        }
                                    }
                                }
                                else
                                {
                                    if (it.skill.isSpawn)
                                    {
                                        var n = Instantiate(it.skill.spawnObject, PlayerEquipent.eq.hand.transform.position, Quaternion.identity);
                                        n.transform.localEulerAngles = transform.localEulerAngles;
                                        n.GetComponent<SpawnebleItem>().keys.AddRange(new List<object>() { null, it.skill.damage * st.damagePercents }); ;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var mobs = GameObject.FindGameObjectsWithTag("Mob");
                            for (int h = 0; h < mobs.Length; h++)
                            {
                                if (Vector3.Distance(transform.position, mobs[h].transform.position) < it.skill.radius)
                                {
                                    mobs[h].GetComponent<Mob>().hp -= it.skill.damage * st.damagePercents;
                                }
                            }
                        }
                        attaked = true;
                    }
                }
                if (it.time <= 0)
                {
                    attaked = false;
                    play = false;
                    it.used = false;
                }
            }
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Can")) return;
        if (play) return;

        for (int i = 1; i < 9; i++)
        {
            if (Input.GetKey(i.ToString()))
            {
                Attack(i);
            }
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            print("Mouse1");
            Attack(1);
        }
    }

    void Attack(int i)
    {
        GetComponent<PlayerController>().point = Vector3.zero;
        var st = GetComponent<PlayerStats>();
        var p = st.skillsPoses.Find(x => x.num == i);
        if (p == null) return;
        if (p.used == false && p.cooldown <= 0)
        {
            if (st.health - p.skill.hp > 1 && st.mana >= p.skill.mana)
            {
                attaked = false;
                st.health -= p.skill.hp;
                st.mana -= p.skill.mana;
                currClip = Random.Range(0, p.skill.clips.Length);
                GetComponent<PlayerController>().enabled = false;
                animator.Play("Idle");
                animator.Play(p.skill.clips[currClip].name);
                p.used = true;
                SkillInvoke.InvokeV(p.skill);
                p.time = p.skill.skillTime;
                p.cooldown = p.skill.coolDown;
                play = true;
            }
        }
    }
}
