using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public GameObject cam;
    Vector3 velocity;
    bool isGrounded;

    public Transform ground;
    public float distance = 0.3f;
    private float yOffSet;

    public float speed;
    private float initialSpeed;
    public float minSpeed;
    public float maxSpeed;

    public float jumpHeight;
    public float gravity;

    public float originalHeight;
    public float crouchHeight;

    public float gettingTiredFactor;

    public LayerMask mask;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        yOffSet = cam.transform.position.y - controller.center.y;
        initialSpeed = speed;
    }

    private void Update()
    {
        //print(speed);
        #region Movement

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal * 0.7f + transform.forward * vertical;
        controller.Move(move*speed*Time.deltaTime);

        #endregion

        #region Crouch

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            transform.DOMoveY(crouchHeight/2,0.08f);
            speed *= 0.2f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = originalHeight;
            transform.DOMoveY(originalHeight / 2, 0.15f);
            speed *= 5;
        }

        #endregion

        #region Run

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(speed == initialSpeed)
            {
                speed = 2 * initialSpeed;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed -= Time.deltaTime * gettingTiredFactor;
            speed = Mathf.Clamp(speed,minSpeed,maxSpeed);
        }

        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    speed = initialSpeed;
        //}

        if (!Input.GetKey(KeyCode.LeftShift))
        {
                speed += gettingTiredFactor * Time.deltaTime;
                speed = Mathf.Clamp(speed, minSpeed,initialSpeed);
        }

        #endregion


        #region Jump

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight - 3.0f * gravity);
        }

        #endregion

        #region Gravity

        isGrounded = Physics.CheckSphere(ground.position, distance, mask);

        if(isGrounded && velocity.y <0)
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        #endregion
    }
}
