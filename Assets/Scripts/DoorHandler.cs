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
        Opening,
        Broken
    }

    public GameObject DoorUI = null;
    public bool dialogueComplete = false;
    private bool brokenBool = false;
    [SerializeField]
    private bool hoveringButton = false;

    [SerializeField]
    private States states = States.Closed;

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float openSize = 8.0f;

    private float sinTime = 0.0f;
    private Vector3 openPos;
    private Vector3 closedPos;

    [SerializeField]
    private List<GameObject> buttons = new List<GameObject>();

    private DialogueManager dialogueManager;

    //colors

    private Color redBase = new Color(0.75f, 0.20f, 0.16f);
    private Color redEmis = new Color(1.0f, 0.22f, 0.22f);
    private Color greenBase = new Color(0.0f, 1.0f, 0.1f);
    private Color greenEmis = new Color(0.46f, 1.0f, 0.59f);
    private Color yellowBase = new Color(1.0f, 0.99f, 0.37f);
    private Color yellowEmis = new Color(1.0f, 0.56f, 0.22f);

    public bool HoveringButton
    {
        get { return hoveringButton; }
        set { hoveringButton = value; }
    }


    public States DoorState
    {
        get { return states; }
    }

   
    // Start is called before the first frame update
    void Start()
    {
        //dialogueManager = FindObjectOfType<DialogueManager>();
        //dialogueManager.OnDialogueEnd += DialogueEnd;
        GetChildButtons(transform.parent);

        DoorUI.SetActive(false);
        closedPos = transform.position;
        Vector3 right = transform.forward * -1;
        openPos = closedPos + right * openSize;

        if (states == States.Open)
        {
            transform.position = openPos;
            SetButtonColor(redBase, redEmis);
        }

        if (states == States.Broken)
        {
            speed = 2.0f;
            SetButtonColor(yellowBase, yellowEmis);
        }
        else if (states == States.Locked)
        {
            SetButtonColor(redBase, redEmis);
        }


        
        //dialogueManager.StartDialogueSequence(0);
    }

    // Update is called once per frame
    void Update()
    {

 

        if ((states == States.Closed || states == States.Open) && hoveringButton)
        {
            DoorUI.SetActive(true);


        }
        else if (checkOtherHovers() == false)
        {
            
            DoorUI.SetActive(false);
        }


        //// opens ui if player has been sitting with the trigger box
        //if (dialogueComplete && inArea)
        //{
        //    DoorUI.SetActive(true);
        //}

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
                        SetButtonColor(redBase, redEmis);
                        sinTime = 0.0f;
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
                        SetButtonColor(greenBase, greenEmis);
                        sinTime = 0.0f;
                    }

                        break;
                }

            case States.Broken:
                {
                    if (!brokenBool && transform.position != openPos)
                    {
                        sinTime += Time.deltaTime * speed;
                        sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                        // sin function
                        float t = 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2f) + 0.5f;
                        transform.position = Vector3.Lerp(closedPos, openPos, t);

                        if (transform.position == openPos)
                        {
                            brokenBool = true;
                            sinTime = 0.0f;
                        }

                    }
                    else if (brokenBool && transform.position != closedPos)
                    {
                        sinTime += Time.deltaTime * speed;
                        sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                        // sin function
                        float t = 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2f) + 0.5f;
                        transform.position = Vector3.Lerp(openPos, closedPos, t);

                        if (transform.position == closedPos)
                        {
                            brokenBool = false;
                            sinTime = 0.0f;
                        }
                    }

                    break;

                }

        }


    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        // if UI is active you can press button
        if(DoorUI.activeInHierarchy && hoveringButton)
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

    private void GetChildButtons(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == "DoorButton")
            {
                buttons.Add(child.gameObject);
            }
        }
    }

    private void SetButtonColor(Color baseColor, Color emisColor)
    {
        foreach(GameObject button in buttons)
        {
            Material m = button.GetComponent<Renderer>().material;
            m.SetColor("_Color", baseColor);
            m.SetColor("_EmissionColor", emisColor);
        }
    }

    // very jank fix for rn
    private bool checkOtherHovers()
    {
        DoorHandler[] doors = transform.parent.parent.GetComponentsInChildren<DoorHandler>();

        foreach (DoorHandler door in doors)
        {
            if (door.hoveringButton)
            {
                //Debug.Log("hovering");
                return true;
            }
        }

        return false;
    }
}
