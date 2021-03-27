using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMainQuest : MonoBehaviour
{
    public bool apply;
    public GameObject canvas, player, panel, startQuest;

    public void StartQuest()
    {
        startQuest.SetActive(false);
        canvas.SetActive(false);
        apply = true;
    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            canvas.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (GetComponent<NPC1>() != null) return;
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            panel.SetActive(true);
            canvas.SetActive(!canvas.active);
        }
    }
}
