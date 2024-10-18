using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueSequence
    {
        public Dialogue[] dialogues;
    }

    [System.Serializable]
    public class Dialogue
    {
        public string characterName;
        public string dialogueText;
        public AudioClip audioClip;
        public bool toContinue;
        public float seconds;
    }

    public DialogueSequence[] dialogueSequences;
    public Canvas dialogueCanvas;
    public TextMeshProUGUI nameTextUI;
    public TextMeshProUGUI dialogueTextUI;
    public AudioSource audioSource;
    public float typewriterSpeed = 0.05f;

    private int currentDialogueIndex = 0;
    private int currentSequenceIndex = -1;

    private void Start()
    {
        // Hide dialogue canvas
        dialogueCanvas.enabled = false;

        // Start the first dialogue sequence
        StartDialogueSequence(0);
    }

    // Method to start a specific dialogue sequence
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

    // Display dialogue for the current sequence
    private IEnumerator DisplayDialogue()
    {
        DialogueSequence currentSequence = dialogueSequences[currentSequenceIndex];

        while (currentDialogueIndex < currentSequence.dialogues.Length)
        {
            Dialogue currentDialogue = currentSequence.dialogues[currentDialogueIndex];

            // Set character name and play audio
            nameTextUI.text = currentDialogue.characterName;
            if (currentDialogue.audioClip != null)
            {
                audioSource.clip = currentDialogue.audioClip;
                audioSource.Play();
            }

            // Display dialogue with typewriter effect
            yield return StartCoroutine(TypewriterEffect(currentDialogue.dialogueText));

            // Wait for audio to finish
            if (currentDialogue.audioClip != null)
                yield return new WaitWhile(() => audioSource.isPlaying);

            // Automatically continue
            if (currentDialogue.toContinue)
            {
                yield return new WaitForSeconds(currentDialogue.seconds);
            }
            else
            {
                // Wait for player input 
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
            }

            currentDialogueIndex++;
        }

        dialogueCanvas.enabled = false;
    }

    // Typewriter effect for displaying dialogue
    private IEnumerator TypewriterEffect(string dialogueText)
    {
        dialogueTextUI.text = "";
        foreach (char letter in dialogueText.ToCharArray())
        {
            dialogueTextUI.text += letter;
            yield return new WaitForSeconds(typewriterSpeed);
        }
    }
}

// Example of starting a dialogue sequence outside of the DialogueManager script
// FindObjectOfType<DialogueManager>().StartDialogueSequence(int);