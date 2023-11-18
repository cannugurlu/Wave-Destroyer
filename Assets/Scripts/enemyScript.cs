using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public int health;
    public bool isDead;
    public bool shouldFollow = true;

    private void Update()
    {
        if (health<=0)
        {
            isDead = true;
        }
        if (isDead)
        {
            shouldFollow = false;
            gameObject.SetActive(false);
        }

        gameObject.transform.LookAt(GameObject.Find("Player").transform.position);
        if (shouldFollow)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, 5*Time.deltaTime);
        }
        else if (!shouldFollow)
        {
            Attack();
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


    void Attack()
    {
        print(gameObject.name + "saldiriyor");
        //saldirma fonksiyonu buraya yazilacak
    }
}
