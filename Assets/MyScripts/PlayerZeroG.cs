using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.ComponentModel.Design.Serialization;
using Cinemachine;
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;


[RequireComponent (typeof(Rigidbody))]

public class PlayerZeroG : MonoBehaviour
{
    [Header("== Player Movement Settings ==")]
    [SerializeField]
    private float rotateSpeedHorizontal = 2.0f;
    [SerializeField]
    private float rotateSpeedVertical = 2.0f;
    [SerializeField]
    private float rollTorque = 1000f;
    [SerializeField]
    private float thrust = 100f;
    [SerializeField]
    private float upThrust = 50f;
    [SerializeField]
    private float strafeThrust = 50f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float leftRightGlideReduction = 0.111f;

    //Propel off bar 
    [SerializeField]
    private float propelThrust = 100f;
    [SerializeField]
    private float propelUpThrust = 50f;
    [SerializeField]
    private float propelStrafeThrust = 50f;

    private float glide = 0f;
    private float verticalGlide = 0f;
    private float horizontalGlide = 0f;

    private Camera mainCam;

    [SerializeField]
    Rigidbody rb;

    //Variables for camera turn and UI interaction
    [SerializeField]
    private GameObject characterPivot;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera vCam;
    [SerializeField]
    private UnityEngine.UI.Image crosshair;

    private Cinemachine.CinemachinePOV pov;

    float horizontalMax;
    float horizontalMin;
    float verticalMax;
    float verticalMin;


    //Input Values
    public InputActionReference grab;

    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;
    private Vector2 pitchYaw;

    // Grabbing mechanic variables
    private bool isGrabbing = false;
    private Transform grabbedBar;
    [SerializeField] 
    private LayerMask barLayer; // Set a specific layer for bars to grab onto
    [SerializeField] 
    private float grabRange = 3f; // Range within which the player can grab bars

    //IK stuff
    [SerializeField]
    IKScript playerIK;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        rb.useGravity = false;
        mainCam = Camera.main;

        // Access the Cinemachine POV component to read axis values
        pov = vCam.GetCinemachineComponent<Cinemachine.CinemachinePOV>();

        // Define the thresholds for axis limits
        horizontalMax = pov.m_HorizontalAxis.m_MaxValue;
        horizontalMin = pov.m_HorizontalAxis.m_MinValue;
        verticalMax = pov.m_VerticalAxis.m_MaxValue;
        verticalMin = pov.m_VerticalAxis.m_MinValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();

        // Get the current horizontal and vertical axis values
        float horizontalAxis = pov.m_HorizontalAxis.Value;
        float verticalAxis = pov.m_VerticalAxis.Value;

        // Check if the player reaches horizontal limits and rotate the mesh accordingly
        if (horizontalAxis >= horizontalMax - 1.0f || horizontalAxis <= horizontalMin + 1.0f)
        {
            RotateMesh(characterPivot.transform.up, horizontalAxis, horizontalMax, horizontalMin, rotateSpeedHorizontal);
        }

        // Check if the player reaches vertical limits and rotate the mesh accordingly
        if (verticalAxis >= verticalMax - 1.0f || verticalAxis <= verticalMin + 1.0f)
        {
            RotateMesh(characterPivot.transform.right, verticalAxis, verticalMax, verticalMin, rotateSpeedVertical);
        }
    }

    //This method will handle all inputs and how they make the ship move
    private void HandleMovement()
    {
        // Get the current horizontal and vertical axis values for the viewport
        float horizontalAxis = pov.m_HorizontalAxis.Value;
        float verticalAxis = pov.m_VerticalAxis.Value;

        //Roll
        //Vector3.back establishes we are rotating on the z-axis
        //Multiplying these together is equivalent to combining the forces
        rb.AddTorque(-characterPivot.transform.forward * roll1D * rollTorque * Time.deltaTime);


        // Thrust Forward (propulsion with cooldown)
        if (thrust1D > 0.1f || thrust1D < -0.1f)
        {
                float currentThrust = thrust;
                rb.AddForce(mainCam.transform.forward * thrust1D * currentThrust * Time.deltaTime);
                glide = thrust;
        }
        else
        {
            //create a force for the glide once no button is being pressed
            rb.AddForce(mainCam.transform.forward * glide * Time.deltaTime);
            //every frame glide will be reduced until it is zero
            glide *= thrustGlideReduction;
        }

        // Up/Down (propulsion with cooldown)
        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            rb.AddForce(mainCam.transform.up * upDown1D * upThrust * Time.fixedDeltaTime);
            verticalGlide = upDown1D * upThrust;
        }
        else
        {
            //create a force for the glide once no button is being pressed
            rb.AddForce(characterPivot.transform.up * verticalGlide * Time.fixedDeltaTime);
            //every frame glide will be reduced until it is zero
            verticalGlide *= upDownGlideReduction;
        }

        // Strafing (propulsion with cooldown)
        if (strafe1D > 0.1f || strafe1D < -0.1f)
        {
            rb.AddForce(mainCam.transform.right * strafe1D * strafeThrust * Time.fixedDeltaTime);
            horizontalGlide = strafe1D * strafeThrust;
        }
        else
        {
            //create a force for the glide once no button is being pressed
            rb.AddForce(mainCam.transform.right * horizontalGlide * Time.fixedDeltaTime);
            //every frame glide will be reduced until it is zero
            horizontalGlide *= leftRightGlideReduction;
        }


        //Propel off bar logic
        if (isGrabbing)
        {
            rb.isKinematic = false;

            //check for WASD interaction to propel
            PropelOffBar();
            pov.m_HorizontalAxis.m_MaxValue = 180;
            pov.m_HorizontalAxis.m_MinValue = -180;
        }
        else if (!isGrabbing)
        {
            pov.m_HorizontalAxis.m_MaxValue = horizontalMax;
            pov.m_HorizontalAxis.m_MinValue = horizontalMin;
        }

    }

    private void RotateMesh(Vector3 rotationAxis, float axisValue, float maxAxis, float minAxis, float rotateSpeed)
    {

        // Rotate when hitting the max limit
        if (axisValue >= minAxis + 1.0f)
        {
            rb.AddTorque(rotationAxis * rotateSpeed * Time.deltaTime);
        }
        // Rotate when hitting the min limit
        else if (axisValue <= minAxis + 1.0f)
        {
            rb.AddTorque(-rotationAxis * rotateSpeed * Time.deltaTime);
        }
    }

    // Try to grab a bar by raycasting
    private void TryGrabBar()
    {

        // Get the position of the crosshair in screen space
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, crosshair.rectTransform.position);

        Ray ray = mainCam.ScreenPointToRay(screenPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabRange, barLayer))
        {
            Debug.Log("Raycast hit: " + hit.transform.name); // Debug the hit

            // Check if the object has the "Grabbable" tag
            if (hit.transform.CompareTag("Grabbable"))
            {
                grabbedBar = hit.transform;
                isGrabbing = true;
                rb.isKinematic = true; // Disable physics while grabbing
                Debug.Log("Grabbing the handle: " + grabbedBar.name);

                // Set the IK target for the right hand
                playerIK.SetRightHandTarget(grabbedBar);
                playerIK.SetRightHandWeight(1f); // Full weight while grabbing
                Debug.Log("Grabbing the handle: " + grabbedBar.name);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything");
        }
    }

    // Release the bar and enable movement again
    private void ReleaseBar()
    {
        isGrabbing = false;
        grabbedBar = null;
        rb.isKinematic = false; // Re-enable physics
        Debug.Log("Released the handle");

        // Reset IK weight to 0
        playerIK.SetRightHandWeight(0f);
    }

    //Player uses WASD to propel themselves faster, only while currently grabbing a bar
    private void PropelOffBar()
    {
        if(isGrabbing)
        {

            Vector3 propelDirection = Vector3.zero;

            if (thrust1D > 0.1f || thrust1D < -0.1f)
            {
                ReleaseBar();
                propelDirection += mainCam.transform.forward * thrust1D * propelThrust;
                Debug.Log("Propelled forward or back");

                // Adjust IK weight to 0.5 (or any value you like) while pulling away
                playerIK.SetRightHandWeight(0.5f);
            }
            else if (upDown1D > 0.1F || upDown1D < -0.1f)
            {
                ReleaseBar();
                propelDirection += mainCam.transform.up * upDown1D * propelUpThrust;
                Debug.Log("Propelled up or down");
            }
            else if (strafe1D > 0.1f || strafe1D < -0.1f)
            {
                ReleaseBar();
                propelDirection += mainCam.transform.right * strafe1D * propelStrafeThrust;
                Debug.Log("Propelled right or left");
            }

            rb.AddForce(propelDirection * Time.deltaTime);
        }
    }

    private void IncreaseLookAroundWhileGrabbing()
    {
        if (isGrabbing)
        {
            pov.m_HorizontalAxis.m_MaxValue = 180;
            pov.m_HorizontalAxis.m_MinValue = -180;
        }
        else if(!isGrabbing)
        {
            pov.m_HorizontalAxis.m_MaxValue = horizontalMax;
            pov.m_HorizontalAxis.m_MinValue = horizontalMin;
        }
    }
    #region Input Methods
    //when we press the buttons on the keyboard or controller these methods pass the buttons through to read the values
    //MUST MANUALLY SET THE CONNECTIONS IN THE EVENTS PANEL ONCE ADDED A PLAYER INPUT COMPONENT
    public void OnThrust(InputAction.CallbackContext context)
    {
        /*once the button, Keyboard or Controller, that is passed through the
         Player Input event to this value of thrust1D*/
        thrust1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }
    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryGrabBar();
        }
        else if (context.canceled)
        {
            ReleaseBar();
        }
    }
    #endregion
}
