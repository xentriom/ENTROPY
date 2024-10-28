using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public string dialogueText;
    public AudioClip audioClip;
}

[System.Serializable]
public class DialogueSequence
{
    public Dialogue[] dialogues;
}
