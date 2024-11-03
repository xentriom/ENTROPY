using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    // Tutorial UI references
    public Canvas tutorialCanvas;
    public TextMeshProUGUI tutorialText;

    // Input and dialogue references
    public InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private DialogueManager dialogueManager;

    // Tutorial sequence tracking
    private int currentStepIndex = 0;
    public float initialDelay = 5f;
    public float delayBetweenSteps = 1f;

    private void Awake()
    {
        // Find the Tutorial action map
        inputActionMap = inputActionAsset.FindActionMap("Tutorial");
    }

    private void Start()
    {
        // Find the DialogueManager
        dialogueManager = FindObjectOfType<DialogueManager>();

        // Hide tutorial canvas and start tutorial sequence
        tutorialCanvas.enabled = false;
        StartCoroutine(TutorialSequence());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator TutorialSequence()
    {
        yield return new WaitForSeconds(initialDelay);
        tutorialCanvas.enabled = true;

        while (currentStepIndex < 4)
        {
            switch (currentStepIndex)
            {
                case 0:
                    yield return StartCoroutine(ShowStepAndWaitForAction("Press F to continue dialogue", "Continue"));
                    break;
                case 1:
                    yield return StartCoroutine(ShowStepAndWaitForAction("Move cursor to rotate camera", "PanCamera"));
                    break;
                case 2:
                    yield return StartCoroutine(ShowStepAndWaitForAction("Hold W to move forward", "MoveForward"));
                    break;
                case 3:
                    yield return StartCoroutine(ShowStepAndWaitForAction("Talk to the terminal to begin", "Interact"));
                    break;
            }
            currentStepIndex++;
            yield return new WaitForSeconds(delayBetweenSteps);
        }

        tutorialCanvas.enabled = false;
        yield return new WaitForSeconds(delayBetweenSteps * 2);
        dialogueManager.StartDialogueSequence(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="actionName"></param>
    /// <returns></returns>
    private IEnumerator ShowStepAndWaitForAction(string instruction, string actionName)
    {
        tutorialText.text = instruction;
        InputAction currentAction = inputActionMap.FindAction(actionName);

        if (currentAction == null)
        {
            yield break;
        }

        inputActionMap.Enable();
        bool actionTriggered = false;

        System.Action<InputAction.CallbackContext> callback = ctx =>
        {
            actionTriggered = true;
        };

        currentAction.performed += callback;

        yield return new WaitUntil(() => actionTriggered);

        currentAction.performed -= callback;
        inputActionMap.Disable();
    }
}