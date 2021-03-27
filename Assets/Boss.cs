using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int attackType;
    public bool attacked;
    public float time, attackCooldown, localtime;
    public GameObject spheres;
    public GameObject player;
    public Animator animator;
    public GameObject sphereRotator, fireFontan;
    public float rotateSpeed;
    public bool waitForEnd;
    public float minDist;
    public float maxHp;
    public RectTransform hp;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDist);
    }
    private void Start()
    {
        maxHp = GetComponent<Mob>().hp;
    }
    private void Update()
    {

        if (GetComponent<Mob>().hp <= 0)
        {
            Application.LoadLevel(2);
        }
        hp.localScale = new Vector3(GetComponent<Mob>().hp / maxHp, 1, 1);
        if (Vector3.Distance(player.transform.position, transform.position) < minDist)
        {
            time += Time.deltaTime;
            if (attackCooldown <= time && attacked == false)
            {
                int newAttack = Random.Range(0, 3);
                while (newAttack == attackType)
                {
                    newAttack = Random.Range(0, 3);
                }
                attackType = newAttack;
                localtime = 0;
                attacked = true; 
                waitForEnd = false;
            }else
            if (attacked)
            {
                time = 0;
                localtime += Time.deltaTime;
                if (attackType == 0)
                {
                    animator.Play("Attack1");
                    if (!waitForEnd)
                        StartCoroutine(wait(20f));
                }
                if (attackType == 1)
                {
                    animator.Play("Attack2");
                    sphereRotator.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
                    if (localtime > 1.5f)
                    {
                        foreach (Transform item in sphereRotator.transform)
                        {
                            Instantiate(spheres.gameObject, item.transform.position, item.transform.rotation);
                        }
                        localtime = 0;
                    }
                    if (!waitForEnd)
                        StartCoroutine(wait(8f));
                }

                if (attackType == 2)
                {
                    animator.Play("Attack2");
                    sphereRotator.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
                    if (localtime > 1.5f)
                    {
                        Instantiate(fireFontan.gameObject, player.transform.position + new Vector3(0,0.6f,0), Quaternion.Euler(-90,0,0));
                        localtime = 0;
                    }
                    if (!waitForEnd)
                        StartCoroutine(wait(20f));
                }
            }
        }
        else
        {
            animator.Play("Idle");
        }
    }


    IEnumerator wait(float _time)
    {
        waitForEnd = true;
        yield return new WaitForSeconds(_time);
        time = 0;
        attacked = false;
    }
}
