using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public GameObject cam;
    Vector3 velocity;
    bool isGrounded;

    public Transform ground;
    public float distance = 0.3f;
    private float yOffSet;

    private bool isUpperStamina;
    private bool isMidStamina;
    private bool isLowerStamina;

    private float currentStamina;
    private float initialSpeed;
    private bool isTiredinStart;

    public float jumpHeight;
    public float gravity;

    public float originalHeight;
    public float crouchHeight;

    public LayerMask mask;

    public Slider slider;

    [Header("Stamina-Speed Values")]
    public float maxStamina;
    public float criticalStamina;
    public float midStamina;
    public float speed;
    public float minSpeed;
    public float midStaminaSpeed;
    public float maxSpeed;
    public float velocityFactor;
    public float staminaFactor;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        yOffSet = cam.transform.position.y - controller.center.y;
        initialSpeed = speed;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        updateBar();
        //Debug.LogError(speed + "            " + currentStamina);

        #region Movement

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal * 0.7f + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        #endregion

        #region Crouch

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            transform.DOMoveY(crouchHeight / 2, 0.08f);
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

            if (currentStamina >= midStamina && currentStamina <= maxStamina) // ilk basista stamina mid staminanin uzerindeyse
            {
                speed = maxSpeed;
                isTiredinStart = false;
            }
            else if (currentStamina < midStamina && currentStamina > criticalStamina) // ilk basista stamina mid staminanin altindaysa
            {
                speed = midStaminaSpeed;
                isTiredinStart = false;
            }

            else if (currentStamina >= 0 && currentStamina <= criticalStamina) // ilk basista stamina critical staminanin altindaysa
            {
                isTiredinStart = true;
            }
        }


        if (Input.GetKey(KeyCode.LeftShift) && !isTiredinStart)
        {
            if (currentStamina >= midStamina && currentStamina <= maxStamina) // basili tutarken yuksek staminaysa
            {
                speed -= velocityFactor * Time.deltaTime;
                speed = Mathf.Clamp(speed, midStaminaSpeed, maxSpeed);

                currentStamina -= staminaFactor * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina,0, maxStamina);

                isUpperStamina = true;
                isMidStamina = false;
                isLowerStamina= false;
            }
            else if(currentStamina < midStamina && currentStamina > criticalStamina) // basili tutarken mid staminaysa
            {
                speed = midStaminaSpeed;

                currentStamina -= staminaFactor * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

                isUpperStamina = false;
                isMidStamina = true;
                isLowerStamina = false;
            }

            else if((currentStamina>=0 && currentStamina <= criticalStamina)) // basili tutarken critic staminaysa
            {
                speed -= 10*velocityFactor * Time.deltaTime;
                speed = Mathf.Clamp(speed, minSpeed, midStaminaSpeed);

                currentStamina -= staminaFactor * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

                isUpperStamina = false;
                isMidStamina = false;
                isLowerStamina = true;
            }

            //if (stamina > 12 && !isTired)
            //{
            //        stamina -= gettingTiredFactor * Time.deltaTime;
            //        stamina = Mathf.Clamp(stamina, 0, 2 * initialSpeed);
            //        speed -= Time.deltaTime * gettingTiredFactor / 1.2f;
            //        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

            //    //hýzý azalt
            //}
            //else if (stamina > 3 && stamina <= 12)
            //{
            //    stamina -= gettingTiredFactor * Time.deltaTime;
            //    stamina = Mathf.Clamp(stamina, 0, 2 * initialSpeed);
            //    isTired = false;
            //    // hýz sabit
            //}
            //else if (stamina > 0 && stamina <= 3)
            //{
            //    stamina -= gettingTiredFactor * Time.deltaTime;
            //    stamina = Mathf.Clamp(stamina, 0, 2 * initialSpeed);
            //    speed -= Time.deltaTime * gettingTiredFactor*4f;
            //    speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            //    isTired = true;
            //    //hýzý hýzlý azalt
            //}
        }
        if(Input.GetKey(KeyCode.LeftShift)&&isTiredinStart)
        {
            currentStamina += 2 * staminaFactor * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            if(currentStamina>criticalStamina) //critical stamina veya mid stamina olarak ayarlanacak
            {
                isTiredinStart = false;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if(currentStamina<maxStamina)
            {
                currentStamina += 2 * staminaFactor * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina,0, maxStamina);
                if(currentStamina>criticalStamina)
                {
                    isUpperStamina=true;
                    isMidStamina = false;
                    isLowerStamina=false;
                }
                if (isUpperStamina)
                {
                    speed = initialSpeed;
                }
                else if (isMidStamina)
                {
                    speed = initialSpeed;
                }
                else if (isLowerStamina)
                {
                    speed += Time.deltaTime * velocityFactor;
                    speed = Mathf.Clamp(speed, minSpeed, initialSpeed);
                }
            }


            //if(stamina<=2*initialSpeed)
            //{
            //    stamina += gettingTiredFactor * Time.deltaTime;
            //    speed += Time.deltaTime * gettingTiredFactor;
            //    speed = Mathf.Clamp(speed, minSpeed, initialSpeed);
            //}
            //if(stamina >= 12)
            //{
            //    isTired = false;
            //}
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

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        #endregion
    }

    void updateBar()
    {
        float fillAmount = Mathf.InverseLerp(0, maxStamina, currentStamina);

        if (slider != null)
        {
            slider.value = fillAmount;
        }
    }
}
