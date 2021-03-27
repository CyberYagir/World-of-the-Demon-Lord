using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Transform player;
    public SphereCollider range;
    public float attackCooldown, attackLength;
    public bool attack, attaked;
    public float minRange;
    public bool triggered, animate = true;
    public float attackDistance;
    public float attackDamage;
    public float attackTime;
    public float speed;
    public Animator animator;
    public GameObject select;
    public float hp = 100;
    public int exp;
    

    private void Start()
    {
        select.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        range.radius = minRange;
        
    }


    private void OnMouseDown()
    {
        if (FindObjectOfType<AttackManager>().selectedMob != null)
        {
            FindObjectOfType<AttackManager>().selectedMob.select.SetActive(false);
        }
        FindObjectOfType<AttackManager>().selectedMob = this;
        FindObjectOfType<AttackManager>().Attack();
        select.SetActive(true);
    }
    float time = 0;
    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
            time += Time.deltaTime;
            GetComponent<MobDrop>().Drop();
            Destroy(gameObject, 10);
            Destroy(this);
            animator.Play("Death");
            return;
        }
        if (triggered)
        {
            attackTime += Time.deltaTime;
            if (!attack)
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
                {
                    if (attackTime >= attackCooldown)
                    {
                        attaked = false;
                        attack = true;
                        attackTime = 0;
                    }
                }
                else
                {
                    if (animate)
                    {
                        animator.Play("Run");
                        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), speed * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (animate)
                {
                    animator.Play("Attack");
                }
                if (attackTime >= attackLength / 2f)
                {
                    if (!attaked)
                    {
                        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
                        {
                            if (player.GetComponent<PlayerStats>() != null)
                            {
                                player.GetComponent<PlayerStats>().health -= attackDamage;
                            }
                            attaked = true;
                        }
                        else
                        {
                            attaked = false;
                            attack = false;
                            attackTime = 0;
                        }
                    }
                }
                if (attackTime >= attackLength)
                {
                    attack = false;
                    attackTime = 0;
                }
            }
        }
        else
        {
            if (animate)
            {
                animator.Play("Idle");
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            range.radius = minRange * 1.6f;
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (FindObjectOfType<AttackManager>().selectedMob == this)
            {
                FindObjectOfType<AttackManager>().selectedMob = null;
            }
            range.radius = minRange;
            triggered = false;
        }
    }
}
