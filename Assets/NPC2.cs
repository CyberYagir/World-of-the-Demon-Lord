using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
    public GameObject player;
    public bool applyedNPC1;
    public GameObject applyBtn, endBtn;
    public GameObject canvas, questPanel;
    public GameObject[] pointsBots, pointsMobs;
    public GameObject[] mobs;
    public GameObject bot;
    public GameObject overlay, lights, barrikades, npcs;
    public Collider[] portals;
    public bool event_, enventEnd;
    public bool endQuest;
    public NPCMainQuestStart mainQuestStart;
    private void OnMouseDown()
    {
        if (endQuest) return;
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            if (applyedNPC1)
            {
                questPanel.SetActive(true);
                canvas.SetActive(!canvas.active);
            }
        }
    }

    public void StartEvent()
    {
        if (enventEnd) return;
        overlay.SetActive(true); applyBtn.SetActive(false);
        StartCoroutine(waitOverlay());
    }
    public void EndQuest()
    {
        PlayerStats.stats.AddExp(560); 
        endQuest = true;
        mainQuestStart.enabled = true;
        canvas.SetActive(false);
        questPanel.SetActive(false);
        Destroy(this);
    }
    public void EndEvent()
    {
        print("EndEvent");
        overlay.SetActive(true);
        overlay.GetComponentInChildren<TMP_Text>().text = "End Battle";
        endBtn.SetActive(true);
        event_ = false; enventEnd = true;
        StartCoroutine(waitEndOverlay());
    }
    public IEnumerator waitEndOverlay()
    {
        print("WaitEnd");
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].isTrigger = true;
        }
        npcs.SetActive(true);
        lights.SetActive(true);
        barrikades.SetActive(false);
        var bots = FindObjectsOfType<NPCAttacker>();
        for (int i = 0; i < bots.Length; i++)
        {
            Destroy(bots[i].gameObject);
        }
        yield return new WaitForSeconds(2);
        overlay.SetActive(false);
        canvas.SetActive(false);
        questPanel.SetActive(false);
    }
    IEnumerator waitOverlay()
    {
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].isTrigger = false;
        }
        yield return new WaitForSeconds(2f);
        npcs.SetActive(false);
        lights.SetActive(false);
        barrikades.SetActive(true);
        
        for (int i = 0; i < pointsBots.Length; i++)
        {
            Instantiate(bot.gameObject, pointsBots[i].transform.position, Quaternion.identity);
        }
        var atts = FindObjectsOfType<NPCAttacker>();
        for (int i = 0; i < pointsMobs.Length; i++)
        {
            var n = Instantiate(mobs[Random.Range(0, mobs.Length)].gameObject, pointsMobs[i].transform.position, Quaternion.identity);
            if (Random.Range(0, 3) != 2)
            {
                n.GetComponent<Mob>().player = atts[Random.Range(0, atts.Length)].transform;
                n.GetComponent<Mob>().triggered = true;
                n.GetComponent<Mob>().attackDamage /= 2;
            }
        }
        event_ = true;
        yield return new WaitForSeconds(1f);
        overlay.SetActive(false);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            canvas.SetActive(false);
        }
    }
}
