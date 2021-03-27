using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 point;
    public Rigidbody rb;
    public float speed;
    public Animator animator;
    PlayerStats st;
    // Start is called before the first frame update
    void Start()
    {
        st = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (point != new Vector3() && Vector2.Distance(point, transform.position) > 0.25f)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Other"))
            {
                animator.Play("Run");
                transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime * st.speedPercents);
            }
            if (GetComponent<AttackManager>().selectedMob == null)
            {
                transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
            }
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Other"))
                animator.Play("Idle");
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (FindObjectsOfType<OverUI>().ToList().FindAll(x => x.over).Count != 0) return;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.transform.tag == "CanWalk")
                    {
                        point = hit.point;
                        if (GetComponent<AttackManager>().selectedMob != null)
                        {
                            GetComponent<AttackManager>().selectedMob.select.SetActive(false);
                            GetComponent<AttackManager>().selectedMob = null;
                        }
                    }
                }
            }
        }
    }
}
