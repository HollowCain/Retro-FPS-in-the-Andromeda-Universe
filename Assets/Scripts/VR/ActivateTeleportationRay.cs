using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftAvtivate;
    public InputActionProperty rightAvtivate;

    // Update is called once per frame
    void Update()
    {
        leftTeleportation.SetActive(leftAvtivate.action.ReadValue<float>() > 0.1f);
        rightTeleportation.SetActive(rightAvtivate.action.ReadValue<float>() > 0.1f);
    }
}
