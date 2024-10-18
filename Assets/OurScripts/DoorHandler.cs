using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Jobs;

public class DoorHandler : MonoBehaviour
{
    public GameObject DoorUI = null;
    private bool isOpen = false;
  

    public float speed = 1.0f;
    private Transform current;
    public Transform target;
    private float sinTime;
   
    // Start is called before the first frame update
    void Start()
    {
        DoorUI.SetActive(false);
        current = transform;

    }


    void OnTriggerEnter(Collider other)
    {
        if (!isOpen && other.tag == "Player")
        {
            DoorUI.SetActive(true);
        }
   
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

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
        if(!isOpen && DoorUI.activeInHierarchy)
        {
            isOpen = true;

        }
    }
}
