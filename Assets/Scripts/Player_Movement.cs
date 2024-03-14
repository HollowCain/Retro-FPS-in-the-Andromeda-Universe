using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    public float playerWalkingSpeed = 5f;
    public float playerRunningSpeed = 15f;
    public float jumpStrenght = 5f;
    public float verticalRotationLimit = 55f;

    float forwardMovement;
    float sidewaysMovement;

    float verticalVelocity;

    float verticalRotation = 0;
    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
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

        if (cc.isGrounded) {
            forwardMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
            }

            if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Dynamic_Crosshair.spread = Dynamic_Crosshair.RUN_SPREAD;
                } else
                {
                    Dynamic_Crosshair.spread = Dynamic_Crosshair.WALK_SPREAD;
                }
            }
        } else
        {
            Dynamic_Crosshair.spread = Dynamic_Crosshair.JUMP_SPREAD;
        }
        

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpStrenght;
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardMovement);

        

        cc.Move(transform.rotation * playerMovement * Time.deltaTime);

    }
}
