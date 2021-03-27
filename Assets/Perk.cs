using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Perk : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public float _maxHPPercent = 1, _maxManaPercent = 1, _cooldownMinPercent = 1, _speedUpPersent = 1, _damagePercent = 1f, _manaRegenPercent = 1;
    public bool applyed;

    private void Update()
    {
        GetComponent<Image>().color = applyed ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (applyed) return;

        if (PlayerStats.stats.skillPoints != 0)
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }
            PlayerStats.stats.healthMax *= _maxHPPercent;
            PlayerStats.stats.manaMax *= _maxManaPercent;
            PlayerStats.stats.cooldownMin *= _cooldownMinPercent;
            PlayerStats.stats.damagePercents *= _damagePercent;
            PlayerStats.stats.speedPercents *= _speedUpPersent;
            PlayerStats.stats.manaPercents *= _manaRegenPercent;
            PlayerStats.stats.skillPoints--;
            applyed = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<PerkMenu>().info.text = (_maxHPPercent != 1 ? "Max Heath Increase: " +  Mathf.Round((_maxHPPercent * 100f) - 100f) + "%\n" : "") + (_maxManaPercent != 1 ? "Max Mana Increase: " + Mathf.Round((_maxManaPercent * 100f) - 100f) + "%\n" : "") + (_cooldownMinPercent != 1 ? "Cooldown Reduce: " + Mathf.Round((_cooldownMinPercent * 100f) - 100f) + "%\n" : "") + (_speedUpPersent != 1 ? "Speed Increase: " + Mathf.Round((_speedUpPersent * 100f) - 100f) + "%\n" : "") + (_damagePercent != 1 ? "Damage Increase: " + Mathf.Round((_damagePercent * 100f) - 100f) + "%\n" : "") + (_manaRegenPercent != 1 ? "Mana regen Increase: " + Mathf.Round((_manaRegenPercent * 100f) - 100f) + "%\n" : "");
    }
}
