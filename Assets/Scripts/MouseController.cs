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

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float rotx = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        float roty = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        xRot -= roty;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        currentRot += rotx;
        currentRot = Mathf.Lerp(currentRot, 0, smoothing * Time.deltaTime);
        print(currentRot);

        transform.localRotation = Quaternion.Euler(xRot, 0f, currentRot);

        body.Rotate(Vector3.up * rotx);
    }
}
