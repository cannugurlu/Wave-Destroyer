using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class CardPrice
{
    public int price;
    public int level;
}

public class CardStats : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    float money;
    public CardPrice [] cardPrices;
    public TextMeshProUGUI [] cardPriceTexts;
    characterGunSystem characterGunSystem;
    public GameObject swordCollider;

    void Start()
    {
        money = 50;
    }

    void Update()
    {
        moneyText.text = money.ToString();
        cardPriceTexts[0].text = cardPrices[0].price.ToString();
        cardPriceTexts[1].text = cardPrices[1].price.ToString();
        cardPriceTexts[2].text = cardPrices[2].price.ToString();
        cardPriceTexts[3].text = cardPrices[3].price.ToString();
        cardPriceTexts[4].text = cardPrices[4].price.ToString();
        cardPriceTexts[5].text = cardPrices[5].price.ToString();
    }

    public void BuyCardStatDamage()
    {
        if (money < cardPrices[0].price || cardPrices[0].level > 2) return;

        money -= cardPrices[0].price;
        cardPrices[0].price += 3;
        cardPrices[0].level += 1;

        if (cardPrices[0].level == 0)
        {
            GameObject.FindGameObjectWithTag("bullet").GetComponent<bulletScript>().bulletsDamage += 10;
            //bulletScript.bulletsDamage += 10;
        }
        else if (cardPrices[0].level == 1)
        {
            GameObject.FindGameObjectWithTag("bullet").GetComponent<bulletScript>().bulletsDamage += 15;
            //bulletScript.bulletsDamage += 15;
        }
        else if (cardPrices[0].level == 2)
        {
            GameObject.FindGameObjectWithTag("bullet").GetComponent<bulletScript>().bulletsDamage += 20;
            //bulletScript.bulletsDamage += 20;
        }
    }

    public void BuyCardStatBulletNumber()
    {
        if (money < cardPrices[1].price || cardPrices[1].level > 2) return;

        money -= cardPrices[1].price;
        cardPrices[1].price += 3;
        cardPrices[1].level += 1;

        if (cardPrices[1].level == 0)
        {
            characterGunSystem.bulletNumber +=10;
        }
        else if (cardPrices[1].level == 1)
        {
            characterGunSystem.bulletNumber += 20;
        }
        else if (cardPrices[1].level == 2)
        {
            characterGunSystem.bulletNumber += 30;
        }
    }

    public void BuyCardStatSwordRange()
    {
        if (money < cardPrices[2].price || cardPrices[2].level > 2) return;

        money -= cardPrices[2].price;
        cardPrices[2].price += 3;
        cardPrices[2].level += 1;

        if (cardPrices[2].level == 0)
        {
            swordCollider.transform.localScale = new Vector3 (0.06428354f, 0.06428354f, 1.1f);
        }
        else if (cardPrices[2].level == 1)
        {
            swordCollider.transform.localScale = new Vector3 (0.06428354f, 0.06428354f, 1.2f);
        }
        else if (cardPrices[2].level == 2)
        {
            swordCollider.transform.localScale = new Vector3 (0.06428354f, 0.06428354f, 1.3f);
        }
    }
    public void BuyCardStat4()
    {
    }
    public void BuyCardStat5()
    {
    }
    public void BuyCardStat6()
    {
    }

}
