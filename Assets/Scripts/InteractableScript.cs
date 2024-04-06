using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableScript : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject NotificationE;

    // Start is called before the first frame update
    void Start()
    {
        NotificationE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            NotificationE.SetActive(true);
            isInRange = true;
            Debug.Log("player is in range");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NotificationE.SetActive(false);
            isInRange = false;
            Debug.Log("player is out of range");
        }
    }
}
