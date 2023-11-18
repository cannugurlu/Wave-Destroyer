using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBlocker : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "spawner")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(spawnerController(other.gameObject));
            //print("x");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "spawner")
        {
            other.gameObject.SetActive(true);
        }
    }

    IEnumerator spawnerController(GameObject obj)
    {
        yield return new WaitForSeconds(0.01f);
        obj.SetActive(true);
    }

}
