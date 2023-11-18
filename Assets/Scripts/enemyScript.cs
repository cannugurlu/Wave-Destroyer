using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public int health;
    public bool isDead;

    private void Update()
    {
        if (health<=0)
        {
            isDead = true;
        }
        if (isDead)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.gameObject.tag == "bullet")
        {
            health -= other.gameObject.GetComponent<bulletScript>().bulletsDamage;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "sword")
        {
            health -= other.gameObject.GetComponent<swordScript>().swordsDamage;
        }
    }

}
