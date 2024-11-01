using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuzzle : MonoBehaviour
{

    public Canvas puzzleCanvas;
    public float interactionDistance = 5f;
    public GameObject player;
    public FPSController fpsController;

    private bool showingPuzzle = false;
    // Start is called before the first frame update
    void Start()
    {
        puzzleCanvas.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance <= interactionDistance)
        {
            //if(Input.GetKeyDown(KeyCode.LeftShift)) {
                //TogglePuzzle();
            //}
        }
    }

    void TogglePuzzle()
    {
        showingPuzzle = !showingPuzzle;
        puzzleCanvas.enabled = showingPuzzle;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = showingPuzzle;
        //fpsController.canMove = !showingPuzzle;
    }
}
