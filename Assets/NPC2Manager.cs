using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2Manager : MonoBehaviour
{
    public NPC2 npc;

    private void Update()
    {
        if (!npc.event_) return;
        if (FindObjectsOfType<Mob>().Length == 0)
        {
            npc.npcs.SetActive(true);
            npc.EndEvent();
            Destroy(this);
        }
    }
}
