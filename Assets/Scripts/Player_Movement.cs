using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;


[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrength = 20f;
    public float verticalRotationLimit = 80f;
    public AudioClip pickupSound;
    public Flashscreen flash;

    float forwardMovement;
    float sidewaysMovement;

    float verticalVelocity;

    float verticalRotation = 0;
    CharacterController cc;
    AudioSource source;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y");
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        if (cc.isGrounded)
        {
            forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
            }
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Dynamic_Crosshair.spread = Dynamic_Crosshair.RUN_SPREAD;
                }
                else
                {
                    Dynamic_Crosshair.spread = Dynamic_Crosshair.WALK_SPREAD;
                }
            }
        }
        else
        {
            Dynamic_Crosshair.spread = Dynamic_Crosshair.JUMP_SPREAD;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpStrength;
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);

        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HpBonus"))
        {
            GetComponent<PlayerHealth>().AddHealth(20);
        }
        else if (other.CompareTag("ArmorBonus"))
        {
            Debug.Log("Took Armor");
            GetComponent<PlayerHealth>().AddArmor(50);
        }
        else if (other.CompareTag("AmmoBonus"))
        {
            transform.Find("Weapons").Find("WeaponHand").GetComponent<Weapon_1>().AddAmmo(15);
        }

        if (other.CompareTag("HpBonus") || other.CompareTag("ArmorBonus") || other.CompareTag("AmmoBonus"))
        {
            flash.PickedUpBonus();
            source.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
        }
    }
}
