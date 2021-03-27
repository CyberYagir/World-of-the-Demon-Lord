using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLocation : MonoBehaviour
{
    public GameObject currLoc;
    public GameObject nextLoc;
    public GameObject spawnNextLoc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine(teleport());
        }
    }

    IEnumerator teleport()
    {
        nextLoc.SetActive(true);
        FindObjectOfType<PlayerController>().transform.position = spawnNextLoc.transform.position;
        currLoc.SetActive(false);
        yield break;
    }
}
