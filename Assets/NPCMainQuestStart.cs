using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCMainQuestStart : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas, applyButton, endButton;
    public List<NPCMainQuest> mainQuests;
    public List<GameObject> npcsIn;
    public bool waitForQuests;
    public GameObject panel;
    public GameObject playerCamera;
    public GameObject demo;
    public GameObject spawnPoint, locFinal, currLoc;
    private void OnMouseDown()
    {
        if (this.enabled == false) return;
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            panel.SetActive(true);
            canvas.SetActive(!canvas.active);
            if (waitForQuests)
            {
                if (mainQuests.ToList().FindAll(x => x.apply).Count == mainQuests.Count)
                {
                    endButton.SetActive(true);
                }
            }
        }
    }
    public void EndQuest()
    {
        playerCamera.SetActive(false);
        demo.SetActive(true);
        StartCoroutine(wait());
        canvas.SetActive(false);
    }
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(11.9f);

        player.transform.position = spawnPoint.transform.position;
        locFinal.SetActive(true);
        demo.SetActive(false);
        playerCamera.SetActive(true);
        currLoc.SetActive(false);
    }
    public void StartQuest()
    {
        for (int i = 0; i < mainQuests.Count; i++)
        {
            mainQuests[i].enabled = true;
        }
        applyButton.active = false;
        waitForQuests = true;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            canvas.SetActive(false);
        }
        if (waitForQuests)
        {
            for (int i = 0; i < mainQuests.Count; i++)
            {
                npcsIn[i].SetActive(mainQuests[i].apply);
            }
        }
    }
}
