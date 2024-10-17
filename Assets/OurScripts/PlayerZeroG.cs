using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.ComponentModel.Design.Serialization;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
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

    [Header("== Grabbing Settings ==")]
    // Grabbing mechanic variables
    private bool isGrabbing = false;
    private Transform grabbedBar;
    [SerializeField]
    private LayerMask barLayer; // Set a specific layer containing bars to grab onto
    [SerializeField]
    private float grabRange = 3f; // Range within which the player can grab bars
    [SerializeField]
    private float grabPadding = 50f;
    //Propel off bar 
    [SerializeField]
    private float propelThrust = 100f;
    [SerializeField]
    private float propelStrafeThrust = 50f;
    private float glide = 0f;
    private float verticalGlide = 0f;
    private float horizontalGlide = 0f;

    [Header("== UI Settings ==")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private TextMeshProUGUI grabUIText;
    [Header("== Camera/Body Refernces ==")]
    private Cinemachine.CinemachinePOV pov;
    private Camera mainCam;
    private bool showTutorialMessages = true;

    //Variables for camera turn and UI interaction
    [SerializeField]
    private GameObject characterPivot;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera vCam;
    [SerializeField]
    private UnityEngine.UI.Image crosshair;
    //FOV references
    private float horizontalMax;
    private float horizontalMin;
    private float verticalMax;
    private float verticalMin;


    //Input Values
    public InputActionReference grab;
    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;
    private Vector2 pitchYaw;

    // Track if the movement keys were released
    private bool movementKeysReleased;

    //Properties
    //this property allows showTutorialMessages to be assigned outside of the script. Needed for the tutorial mission
    public bool ShowTurorialMessages
    {
        get { return showTutorialMessages; }
        set { showTutorialMessages = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //lock the mouse to the viewport
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
        // Get the current horizontal and vertical axis values
        float horizontalAxis = pov.m_HorizontalAxis.Value;
        float verticalAxis = pov.m_VerticalAxis.Value;

        if (!isGrabbing)
        {
            //handle how the player moves in open space
            HandleFreeMovement();

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
        else if (isGrabbing)
        {
            HandleGrabMovement(horizontalAxis, verticalAxis);
        }

        GrabUIText();
    }

    //This method will handle all inputs and how they make the ship move
    private void HandleFreeMovement()
    {
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
    }

    private void HandleGrabMovement(float horizontalAxisPos, float verticalAxisPos)
    {
        //Propel off bar logic
        if (isGrabbing)
        {
            // Check if the player reaches horizontal limits and rotate the mesh accordingly
            if (horizontalAxisPos >= horizontalMax - 1.0f || horizontalAxisPos <= horizontalMin + 1.0f)
            {
                //allow to look around
                RotateMesh(characterPivot.transform.up, horizontalAxisPos, horizontalMax, horizontalMin, rotateSpeedHorizontal);
            }

            PropelOffBar();
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

        // Apply padding to the screen point
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector2 paddedMin = new Vector2(screenPoint.x - grabPadding, screenPoint.y - grabPadding);
        Vector2 paddedMax = new Vector2(screenPoint.x + grabPadding, screenPoint.y + grabPadding);

        for(float x = paddedMin.x; x < paddedMax.x; x += grabPadding / 2)
        {
            for(float y = paddedMin.y; y < paddedMax.y; y += grabPadding / 2)
            {
                Ray ray = mainCam.ScreenPointToRay(new Vector3(x, y, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, grabRange, barLayer))
                {
                    Debug.Log("Raycast hit: " + hit.transform.name); // Debug the hit

                    // Check if the object has the "Grabbable" tag
                    if (hit.transform.CompareTag("Grabbable"))
                    {
                        grabbedBar = hit.transform;
                        isGrabbing = true;

                        // Stop movement by reducing velocity and angular velocity
                        rb.velocity = rb.velocity * 0.1f;  // Reduce velocity to 10% of its current value
                        rb.angularVelocity = Vector3.zero;  // Stop rotation completely

                        Debug.Log("Grabbing the handle: " + grabbedBar.name);
                        return;
                    }
                }
            }
        }
        Debug.Log("Raycast did not hit anything");
    }

    private bool IsInRangeofBar()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, crosshair.rectTransform.position);
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Vector2 paddedMin = new Vector2(screenPoint.x - grabPadding, screenPoint.y - grabPadding);
        Vector2 paddedMax = new Vector2(screenPoint.x + grabPadding, screenPoint.y + grabPadding);

        for (float x = paddedMin.x; x < paddedMax.x; x += grabPadding / 2)
        {
            for (float y = paddedMin.y; y < paddedMax.y; y += grabPadding / 2)
            {
                Ray ray = mainCam.ScreenPointToRay(new Vector3(x, y, 0));
                if (Physics.Raycast(ray, grabRange, barLayer))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Release the bar and enable movement again
    private void ReleaseBar()
    {
        isGrabbing = false;
        grabbedBar = null;
        Debug.Log("Released the handle");

    }

    private void GrabUIText()
    {
        if (showTutorialMessages)
        {
            if (isGrabbing)
            {
                grabUIText.text = "use 'WASD' to propel forwards, back, left, right";
            }
            else if (IsInRangeofBar() && !isGrabbing)
            { 
                grabUIText.text = "press and hold 'Right Mouse Button' to grab and hold onto";
            }
            else if(!isGrabbing && !IsInRangeofBar())
            {
                grabUIText.text = null;
            }
        }
        else if (!showTutorialMessages)
        {
            return;
        }
    }

    //Player uses WASD to propel themselves faster, only while currently grabbing a bar
    private void PropelOffBar()
    {
        //if the player is grabbing and no movement buttons are currently being pressed
        if(isGrabbing)
        {
            // Check if no movement buttons are currently being pressed
            bool isThrusting = Mathf.Abs(thrust1D) > 0.1f;
            bool isStrafing = Mathf.Abs(strafe1D) > 0.1f;

            if(movementKeysReleased && (isThrusting || isStrafing))
            {
                //initialize a vector 3 for the propel direction
                Vector3 propelDirection = Vector3.zero;

                //if W or S are pressed
                if (isThrusting)
                {
                    //release the bar and calculate the vector to propel based on the forward look
                    ReleaseBar();
                    propelDirection += mainCam.transform.forward * thrust1D * propelThrust;
                    Debug.Log("Propelled forward or back");
                }
                //if A or D are pressed
                else if (isStrafing)
                {
                    //release the bar and calculate the vector to propel based on the right look
                    ReleaseBar();
                    propelDirection += mainCam.transform.right * strafe1D * propelStrafeThrust;
                    Debug.Log("Propelled right or left");
                }
                //add the propel force to the rigid body
                rb.AddForce(propelDirection * Time.deltaTime);
                // Set the flag to false since keys are now pressed
                movementKeysReleased = false;
            }
            // Update the flag if no movement keys are pressed
            else if (!isThrusting && !isStrafing)
            {
                movementKeysReleased = true;
            }
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

    void OnDrawGizmos()
    {
        // Visualize the crosshair padding as a box in front of the camera
        if (mainCam != null)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, crosshair.rectTransform.position);

            // Define padded bounds
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            Vector2 paddedMin = new Vector2(screenPoint.x - grabPadding, screenPoint.y - grabPadding);
            Vector2 paddedMax = new Vector2(screenPoint.x + grabPadding, screenPoint.y + grabPadding);

            // Draw a box at the grab range with padding
            Gizmos.color = Color.green;
            for (float x = paddedMin.x; x <= paddedMax.x; x += grabPadding / 2)
            {
                for (float y = paddedMin.y; y <= paddedMax.y; y += grabPadding / 2)
                {
                    Ray ray = mainCam.ScreenPointToRay(new Vector3(x, y, 0));
                    Gizmos.DrawRay(ray.origin, ray.direction * grabRange);
                }
            }
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
