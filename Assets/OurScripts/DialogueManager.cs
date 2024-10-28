using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Dialogue sequences and UI references
    public DialogueSequence[] dialogueSequences;
    public Canvas dialogueCanvas;
    public TextMeshProUGUI nameTextUI;
    public TextMeshProUGUI dialogueTextUI;
    public AudioSource audioSource;
    public float typewriterSpeed = 0.08f;

    // Events and input control
    public event Action OnDialogueEnd;
    private PlayerController playerController;

    // Dialogue sequence tracking
    private int currentDialogueIndex = 0;
    private int currentSequenceIndex = -1;
    private bool isSkipping = false;

    // Initialization
    private void Awake()
    {
        playerController = new PlayerController();
    }

    private void OnEnable() => playerController.Dialogue.ContinueDialogue.Enable();
    private void OnDisable() => playerController.Dialogue.ContinueDialogue.Disable();

    private void Start()
    {
        // Hide dialogue canvas on start
        dialogueCanvas.enabled = false;
    }

    private void Update()
    {
        // Check for skip input each frame
        if (playerController.Dialogue.ContinueDialogue.triggered)
        {
            isSkipping = true;
        }
    }

    /// <summary>
    /// Starts a dialogue sequence by index
    /// </summary>
    /// <param name="sequenceIndex">The sequence to start</param>
    public void StartDialogueSequence(int sequenceIndex)
    {
        if (sequenceIndex < dialogueSequences.Length)
        {
            currentSequenceIndex = sequenceIndex;
            currentDialogueIndex = 0;
            dialogueCanvas.enabled = true;
            StartCoroutine(DisplayDialogue());
        }
    }

    /// <summary>
    /// Displays the dialogue sequence on the UI
    /// </summary>
    /// <returns>IEnumerator for coroutine control</returns>
    private IEnumerator DisplayDialogue()
    {
        DialogueSequence currentSequence = dialogueSequences[currentSequenceIndex];

        while (currentDialogueIndex < currentSequence.dialogues.Length)
        {
            // Get current dialogue and set name
            Dialogue currentDialogue = currentSequence.dialogues[currentDialogueIndex];
            nameTextUI.text = currentDialogue.characterName;

            // Play audio clip if available
            if (currentDialogue.audioClip != null)
            {
                audioSource.clip = currentDialogue.audioClip;
                audioSource.Play();
            }

            // Display dialogue with typewriter effect
            yield return StartCoroutine(TypewriterEffect(currentDialogue.dialogueText, currentDialogue.audioClip));

            // Grace period for skipping
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => playerController.Dialogue.ContinueDialogue.triggered);

            if (audioSource.isPlaying) audioSource.Stop();
            currentDialogueIndex++;
        }

        // Hide canvas and invoke end event
        dialogueCanvas.enabled = false;
        OnDialogueEnd?.Invoke();
    }

    /// <summary>
    /// Displays the dialogue text using a typewritter effect
    /// </summary>
    /// <param name="dialogueText">The text to display with the effect</param>
    /// <param name="audioClip">Audio clip to set typewritting speed</param>
    /// <returns>IEnumerator for coroutine control</returns>
    private IEnumerator TypewriterEffect(string dialogueText, AudioClip audioClip)
    {
        dialogueTextUI.text = "";
        isSkipping = false;

        // Determine typewriter speed based on audio clip length if available
        if (audioClip != null)
        {
            typewriterSpeed = audioClip.length / dialogueText.Length;
        }

        foreach (char letter in dialogueText.ToCharArray())
        {
            dialogueTextUI.text += letter;
            yield return new WaitForSeconds(typewriterSpeed);

            if (isSkipping)
            {
                dialogueTextUI.text = dialogueText;
                isSkipping = false;
                yield break;
            }
        }
    }
}

// Example of starting a dialogue sequence outside of the DialogueManager script
// private DialogueManager dialogueManager;
// dialogueManager = FindObjectOfType<DialogueManager>();
// dialogueManager.StartDialogueSequence(int);
// dialogueManager.OnDialogueEnd += exampleFunction;
//
// private void exampleFunction()
// {
//     Debug.Log("Dialogue sequence ended");
//     // Additional logic here (open doors, tp alien, blow up map, etc.)
// }