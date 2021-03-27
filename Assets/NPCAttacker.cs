using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCAttacker : MonoBehaviour
{
    public float speed;
    public Transform target;
    public Animator animator;
    public string attack;
    float time;
    private void Update()
    {
        if (target == null)
        {
            animator.Play("Idle");
            var mb = FindObjectsOfType<Mob>();
            if (mb.Length == 0)
            {
                target = null;
                return;
            }
            target = mb[Random.Range(0, mb.Length)].transform;
        }
        else
        {
            if (Vector3.Distance(target.transform.position, transform.position) > 1)
            {
                animator.Play("Run");
                time = 1f;
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z), speed * Time.deltaTime);
            }
            else
            {
                time += Time.deltaTime;
                if (time >= 1f)
                {
                    if (target == null) return;
                    if (target.GetComponent<Mob>() == null){ target = null; return; }
                    target.GetComponent<Mob>().player = transform;
                    target.GetComponent<Mob>().hp -= 5f;
                    target.GetComponent<Mob>().triggered = true;
                    animator.Play(attack);
                    time = 0;
                }
            }
        }
    }
}
