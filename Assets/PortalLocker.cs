using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLocker : MonoBehaviour
{
    public GameObject text;

    private void Update()
    {
        text.SetActive(PlayerStats.stats.lvl < 3);
        GetComponent<Collider>().isTrigger = !text.active;
    }
}
