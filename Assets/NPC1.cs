using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public GameObject questPanel,  questPanel1;
    public GameObject[] spawns;
    public GameObject goblin;
    public bool apply, apply1;
    public List<Mob> spawnedGoblins;
    public GameObject applyButton, applyButton1;
    public GameObject endButton, endButton1;
    public NPC2 npc2;
    public GameObject questIcon;

    private void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            if (questIcon != null)
            {
                Destroy(questIcon.gameObject);
            }
            canvas.SetActive(!canvas.active);
        }
    }

    private void Update()
    {
        if (GetComponent<NPCMainQuest>() != null)
        {
            GetComponent<NPCMainQuest>().enabled = false;
        }
        if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            canvas.SetActive(false);
        }
        if (apply)
        {
            if (spawnedGoblins.Find(x=>x != null) == null)
            {
                endButton.SetActive(true);
            }
        }
        if (npc2.endQuest)
        {
            endButton1.SetActive(true);
        }
    }

    public void ApplyQuest1()
    {
        apply1 = true;
        npc2.applyedNPC1 = true;
        applyButton1.SetActive(false);
    }
    public void EndQuest1()
    {
        canvas.SetActive(false);
        player.GetComponent<PlayerStats>().exp += 350;
        questPanel.SetActive(false);
        questPanel1.SetActive(false);
        GetComponent<NPCMainQuest>().enabled = true;
        Destroy(this);
    }
    public void ApplyQuest()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            spawnedGoblins.Add(Instantiate(goblin, spawns[i].transform.position, Quaternion.Euler(0, Random.Range(0, 360f), 0)).GetComponent<Mob>());
        }
        apply = true;
        applyButton.SetActive(false);
    }

    public void EndQuest()
    {
        canvas.SetActive(false);
        player.GetComponent<PlayerStats>().exp += 100;
        questPanel.SetActive(false);
        questPanel1.SetActive(true);
    }
}
