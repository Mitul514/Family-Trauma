using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private Text sentenceTxt, nameTxt;
    [SerializeField] private GameObject continueBtn;

    public Action onDialogueEnd;
    private DialogueScriptable m_scriptable;
    private Queue<Dialogue> dialogues = new Queue<Dialogue>();

    public void StartDialogue(DialogueScriptable scriptable)
    {
        if (m_scriptable == null)
            m_scriptable = scriptable;

        Dialogue[] dialogue = scriptable.dialogue;
        dialogues.Clear();

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogues.Enqueue(dialogue[i]);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        continueBtn.SetActive(false);
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
        continueBtn.SetActive(true);
    }

    private void EndDialogue()
    {
        print("End ..... ");
        gameObject.SetActive(false);
        onDialogueEnd?.Invoke();
    }
}
