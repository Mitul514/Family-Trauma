using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private Text sentenceTxt, nameTxt;
    [SerializeField] private GameObject dialoguePanel;

    public Action onDialogueEnd;

    private Queue<Dialogue> dialogues = new Queue<Dialogue>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        DontDestroyOnLoad(this);
    }

    public void StartDialogue(Dialogue[] dialogue)
    {
        dialogues.Clear();

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogues.Enqueue(dialogue[i]);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }
        Dialogue sentence = dialogues.Dequeue();
        StartCoroutine(TypeSentence(sentence.Speech[0], sentence.Name));
    }

    private IEnumerator TypeSentence(string msg, string name)
    {
        nameTxt.text = name;
        sentenceTxt.text = " ";
        foreach (char letter in msg.ToCharArray())
        {
            sentenceTxt.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        print("End ..... ");
        onDialogueEnd?.Invoke();
    }
}
