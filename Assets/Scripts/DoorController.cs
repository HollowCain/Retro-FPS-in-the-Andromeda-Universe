using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("Door is open");
            //animator.SetBool("isOpen", isOpen);
            SceneManager.LoadScene("Main_tutorial_scene");
        }

    }
}
