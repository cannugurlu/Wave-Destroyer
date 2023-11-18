using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && !transform.parent.GetComponent<enemyScript>().isDead)
        {
            gameObject.transform.parent.GetComponent<enemyScript>().shouldFollow = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && !transform.parent.GetComponent<enemyScript>().isDead)
        {
            gameObject.transform.parent.GetComponent<enemyScript>().shouldFollow = true;
        }
    }
}
