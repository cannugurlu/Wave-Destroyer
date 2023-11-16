using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class characterGunSystem : MonoBehaviour
{
    public GameObject gun,sword,bullet,handPos;
    public int bulletNumber;
    public float bulletSpeed;
    bool isShooting=false;
    bool isSwordMoving = false;
    bool isSword = false;
    bool isLeft = false;

    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        handPos = GameObject.Find("HandPosition");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if((!isShooting && bulletNumber>0) || isSword)
            {
                StartCoroutine(Shoot());
            }
            if (bulletNumber == 0)
            {
                gunChange();
            }
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        if (!isSword)
        {
            GameObject _bullet = Instantiate(bullet, GameObject.Find("spawner").transform.position, Quaternion.identity);
            _bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

            Vector3 initialRot = handPos.transform.eulerAngles;
            handPos.transform.DOLocalRotate(new Vector3(-10, 0, 0), 0.3f).OnComplete(() =>
            handPos.transform.DOLocalRotate(new Vector3(0, 0, 0), 1));

            bulletNumber--;
            textMeshProUGUI.text = bulletNumber.ToString();
            if (bulletNumber <= 0)
            {
                textMeshProUGUI.enabled = false;
                isSword = true;
            }
        }
        if(isSword && !isSwordMoving)
        {
            switch(isLeft)
            {
                case true:
                    //saga gitme kodu -30/30/0
                    isSwordMoving = true;
                    sword.transform.DOLocalRotate(new Vector3(-30, 30, 0), 0.2f).OnComplete(()=> isSwordMoving=false);
                    isLeft = false;
                    break;
                case false:
                    //sola gitme kodu -10/-40/30
                    isSwordMoving = true;
                    sword.transform.DOLocalRotate(new Vector3(-10, -40, 30), 0.2f).OnComplete(()=> isSwordMoving=false);
                    isLeft = true;
                    break;
            }
        }

        isShooting=false;
        yield return null;
    }

    void gunChange()
    {
        gun.transform.DOLocalRotate(new Vector3(20, 0, 0), 1.2f);
        gun.transform.DOLocalMove(new Vector3(0, -3, -0.5f), 1.2f).OnComplete(() =>
        {
            sword.transform.DOLocalMove(handPos.transform.localPosition, 0.4f);
            sword.transform.DOLocalRotate(new Vector3(-30,30,0), 0.4f);
        });

    }
}
