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
        Locked,
        Closed,
        Closing,
        Open,
        Opening
    }


    public GameObject DoorUI = null;
    public bool dialogueComplete = false;
    private bool inArea = false;

    [SerializeField]
    private States states = States.Closed;

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float openSize = 8.0f;

    private float sinTime;
    private Vector3 openPos;
    private Vector3 closedPos;

    private DialogueManager dialogueManager;


    public States DoorState
    {
        get
        {
            return states;
        }
    }

   
    // Start is called before the first frame update
    void Start()
    {
        //dialogueManager = FindObjectOfType<DialogueManager>();
       //dialogueManager.OnDialogueEnd += DialogueEnd;

        DoorUI.SetActive(false);
        closedPos = transform.position;
        Vector3 right = transform.forward * -1;
        openPos = closedPos + right * openSize;
        Debug.Log(openPos);

        if (states == States.Open)
        {
            transform.position = openPos;
        }

        
        //dialogueManager.StartDialogueSequence(0);
    }


    void OnTriggerEnter(Collider other)
    {

        Debug.Log(states.ToString());

        if (states == States.Closed && other.tag == "Player")
        {
            Debug.Log("here");
            DoorUI.SetActive(true);
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
                    if (transform.position != openPos)
                    {
                        sinTime += Time.deltaTime * speed;
                        sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                        // sin function
                        float t = 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2f) + 0.5f;
                        transform.position = Vector3.Lerp(closedPos, openPos, t);
                    }
                    else
                    {
                        states = States.Open;
                    }


                    break;
                }

            case States.Closing:
                {
                    if (transform.position != closedPos)
                    {
                        sinTime += Time.deltaTime * speed;
                        sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                        // sin function
                        float t = 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2f) + 0.5f;
                        transform.position = Vector3.Lerp(openPos, closedPos, t);
                    }
                    else
                    {
                        states = States.Closed;
                    }

                        break;
                }

        }


    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        // if UI is active you can press button
        if(DoorUI.activeInHierarchy && inArea == true)
        {
            if (states == States.Open)
            {
                states = States.Closing;
            }
            else if (states == States.Closed)
            {
                states = States.Opening;
            }
            

        }
    }

    private void DialogueEnd(int sequenceIndex)
    {
        // Only open door if the first dialogue sequence is completed
        if (sequenceIndex == 0)
        {
            Debug.Log($"Dialogue {sequenceIndex} completed, door can be opened.");
            dialogueComplete = true;
        }
    }

    public void PuzzleComplete()
    {
        if(states == States.Locked)
        {
            states = States.Opening;
        }
    }
}
