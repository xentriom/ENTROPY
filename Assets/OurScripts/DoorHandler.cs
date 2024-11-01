using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

public class DoorHandler : MonoBehaviour
{

    public enum States
    {
        Closed,
        Closing,
        Open,
        Opening
    }


    public GameObject DoorUI = null;
    public bool dialogueComplete = false;
    private bool inArea = false;
  

    public States states = States.Closed;

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
      
        if (states == States.Closed && other.tag == "Player")
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


        // opens ui if player has been sitting with the trigger box
        if (dialogueComplete && inArea)
        {
            DoorUI.SetActive(true);
        }

        // handles different door interactions
        switch(states)
        {
            case States.Opening:
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
                        states = States.Open;
                    }


                    break;
                }

            case States.Closing:
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
                        states = States.Open;
                    }

                    break;
                }

        }


    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        // if UI is active you can press button
        if(DoorUI.activeInHierarchy)
        {
            states = States.Opening;

        }
    }

    private void DialogueEnd()
    {
        Debug.Log("Dialogue completed, door can be opened.");
        dialogueComplete = true;
    }
}
