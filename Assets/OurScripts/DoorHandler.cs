using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

public class DoorHandler : MonoBehaviour
{
    public GameObject DoorUI = null;
    public bool dialogueComplete = false;
    private bool isOpen = false;
    private bool inArea = false;
  

    public float speed = 1.0f;
    private Transform current;
    public Transform target;
    private float sinTime;

    private DialogueManager dialogueManager;

   
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnDialogueEnd += DialogueEnd;
        DoorUI.SetActive(false);
        current = transform;
        dialogueManager.StartDialogueSequence(0);
    }


    void OnTriggerEnter(Collider other)
    {
      
        if (!isOpen && other.tag == "Player")
        {
            inArea = true;
        }
   
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorUI.SetActive(false);
            inArea = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueComplete && inArea && DoorUI.active == false)
        {
            DoorUI.SetActive(true);
        }

        if (isOpen)
        {
            if (transform.position != target.position)
            {
                sinTime += Time.deltaTime * speed;
                sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                // sin function
                float t = 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2f) + 0.5f;
                transform.position = Vector3.Lerp(current.position, target.position, t);
            }
            else
            {
                // future update to open and close the door
                // this keeps track of when the door has fully opened to change a variable
                //openDoor = false;
            }
        }

    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        // if UI is active you can press button
        if(DoorUI.activeInHierarchy)
        {
            isOpen = true;

        }
    }

    private void DialogueEnd()
    {
        dialogueComplete = true;
    }
}
