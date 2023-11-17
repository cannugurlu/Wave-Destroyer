using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [Range(50f, 500f)]
    public float sens;

    public Transform body;

    float xRot = 0f;

    public float smoothing;
    float currentRot;

    public Vector3 whileZoomingPos;
    public Vector3 whileZoomingRot;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private Vector3 initialRot;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        initialPos = gameObject.transform.Find("HandPosition").transform.localPosition;
        initialRot = gameObject.transform.Find("HandPosition").transform.localEulerAngles;
        whileZoomingRot = initialRot;
        whileZoomingRot.x = 20.0f;
    }
    private void Update()
    {
        float rotx = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        float roty = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        xRot -= roty;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        currentRot += rotx;
        currentRot = Mathf.Lerp(currentRot, 0, smoothing * Time.deltaTime);
        //print(currentRot);

        transform.localRotation = Quaternion.Euler(xRot, 0f, currentRot);

        body.Rotate(Vector3.up * rotx);

        #region Zoom

        if (Input.GetMouseButtonDown(1))
        {
            ZoomIn();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ZoomOut();
        }

        #endregion
    }

    void ZoomIn()
    {
        gameObject.transform.Find("HandPosition").transform.DOLocalMoveX(whileZoomingPos.x, 0.2f);
        gameObject.transform.Find("HandPosition").transform.DOLocalMoveY(initialPos.y+0.05f, 0.2f);

        gameObject.transform.Find("HandPosition").transform.DOLocalRotate(whileZoomingRot, 0.2f);

        gameObject.GetComponent<Camera>().fieldOfView = 40;
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.Clamp(gameObject.GetComponent<Camera>().fieldOfView, 30, 60);

        gameObject.transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y+0.05f, transform.localPosition.z);
    }
    void ZoomOut()
    {
        gameObject.transform.Find("HandPosition").transform.DOLocalMoveX(initialPos.x, 0.2f);
        gameObject.transform.Find("HandPosition").transform.DOLocalMoveY(initialPos.y, 0.2f);

        gameObject.transform.Find("HandPosition").transform.DOLocalRotate(initialRot, 0.2f);

        gameObject.GetComponent<Camera>().fieldOfView = 60;
        gameObject.GetComponent<Camera>().fieldOfView = Mathf.Clamp(gameObject.GetComponent<Camera>().fieldOfView, 30, 60);

        gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.05f, transform.localPosition.z);
    }
}
