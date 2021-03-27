using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject load;
    public void Play()
    {
        if (!Application.isLoadingLevel)
        {
            load.SetActive(true);
            Application.LoadLevel(1);
        }
    }

    public void OpenClose(GameObject gm)
    {
        gm.active = !gm.active;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
