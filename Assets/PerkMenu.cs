using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PerkMenu : MonoBehaviour
{
    public TMP_Text info;
    public TMP_Text skillsCount;

    private void Update()
    {
        skillsCount.text = "Perk points: " + PlayerStats.stats.skillPoints;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
