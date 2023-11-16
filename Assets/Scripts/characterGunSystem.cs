using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class characterGunSystem : MonoBehaviour
{
    [SerializeField] Vector3 rightSwordPos, leftSwordPos, rightSwordRot, leftSwordRot;

    public GameObject gun,sword,bullet,handPos;
    public int bulletNumber;
    public float bulletSpeed;
    bool isShooting=false;
    bool isSwordMoving = false;
    bool isSword = false;
    bool isRight = false;
    bool gunChangeController = true;
    bool isGunChanging = false;

    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        handPos = GameObject.Find("HandPosition");
        textMeshProUGUI.text = bulletNumber.ToString();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if((!isShooting && bulletNumber>0) || (isSword && !isSwordMoving))
            {
                if (!isGunChanging)
                {
                    StartCoroutine(Shoot());
                }
            }
            if (bulletNumber == 0 && gunChangeController)
            {
                gunChange();
                print("gun change");

            }
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        if (!isSword)
        {
            print("gun fonksiyonu calisti");
            GameObject _bullet = Instantiate(bullet, GameObject.Find("spawner").transform.position, Quaternion.identity);
            StartCoroutine(bulletClear(_bullet));
            _bullet.GetComponent<Rigidbody>().velocity = gun.transform.forward * bulletSpeed;

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
            print("sword if blogu");
            swordFunc();
            isRight = !isRight;
        }

        isShooting=false;
        yield return null;
    }

    void swordFunc()
    {
        print("swordFunc");
        isSwordMoving = true;
        if (isRight)
        {
            print("sola");
            sword.transform.DOLocalMove(leftSwordPos, 0.3f);
            sword.transform.DOLocalRotate(leftSwordRot, 0.3f).OnComplete(() => isSwordMoving = false);
        }
        else
        {
            print("saga");
            sword.transform.DOLocalMove(rightSwordPos, 0.3f);
            sword.transform.DOLocalRotate(rightSwordRot, 0.3f).OnComplete(() => isSwordMoving = false);
        }
    }

    void gunChange()
    {
            isGunChanging = true;
            gunChangeController = false;
            gun.transform.DOLocalRotate(new Vector3(20, 0, 0), 1.2f);
            gun.transform.DOLocalMove(new Vector3(0, -3, -0.5f), 1.2f).OnComplete(() =>
            {
                sword.transform.DOLocalMove(handPos.transform.localPosition, 0.4f);
                sword.transform.DOLocalRotate(new Vector3(-30, 30, 0), 0.4f).OnComplete(()=>isGunChanging= false);
            });
    }

    IEnumerator bulletClear(GameObject _bullet)
    {
        yield return new WaitForSeconds(7.0f);
        if(_bullet != null)
            Destroy(_bullet);
    }

}
