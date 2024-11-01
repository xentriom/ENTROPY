using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerPuzzle : MonoBehaviour
{

    public Canvas puzzleCanvas;
    public float interactionDistance = 5f;
    public GameObject player;
    //public FPSController fpsController;
    PlayerController playerController;
    //public Canvas playerUI;

    private bool showingPuzzle = false;

    public GameObject crosshair; // Reference to the crosshair image
    public GameObject interactionText; // Reference to the TextMeshPro component for the interaction message

    private void Awake()
    {
        // Initialize the PlayerController and the Interact action
        playerController = new PlayerController();
        playerController.PlayerControls.Interact.performed += OnInteract; // Hook up Interact to your function
    }
    void Start()
    {
        puzzleCanvas.enabled = false;

        StartCoroutine(CheckDistance()); // Start the distance checking coroutine

    }

    private void OnEnable()
    {
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // Only proceed if within interaction range
        if (Vector3.Distance(player.transform.position, transform.position) <= interactionDistance)
        {
            TogglePuzzle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CheckDistance()
    {
        while (true) // Infinite loop
        {
            // Check distance to show/hide interaction text
            if (Vector3.Distance(player.transform.position, transform.position) <= interactionDistance)
            {
                //interactionText.GetComponnent<
                    //text = "Z to interact"; // Show the interaction message
            }
            else
            {
                //interactionText.enabled = false; // Hide the interaction message
            }

            yield return new WaitForSeconds(0.5f); // Wait for half a second before checking again
        }
    }

    public void TogglePuzzle()
    {
        showingPuzzle = !showingPuzzle;
        puzzleCanvas.enabled = showingPuzzle;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = showingPuzzle;
        //fpsController.canMove = !showingPuzzle;
    }
}
