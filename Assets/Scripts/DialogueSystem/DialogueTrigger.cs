using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected DialogueScriptable dialogueSet;
    [SerializeField] protected Button continueBtn;
    [SerializeField] protected DialogueManager dialogueManager;

    public Action OnTriggereDialogueEnd;
    public bool isThisDialogueSetPlayed => dialogueSet.isPlayed;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(TriggerDialogue);
        dialogueManager.onDialogueEnd += onDialogueEnd;
    }

    private void OnDisable()
    {
        continueBtn?.onClick.RemoveListener(TriggerDialogue);
        dialogueManager.onDialogueEnd -= onDialogueEnd;
    }

    protected virtual void onDialogueEnd()
    {
        dialogueManager.gameObject.SetActive(false);
        OnTriggereDialogueEnd?.Invoke();
        dialogueSet.isPlayed = true;
        gameObject.SetActive(false);
    }

    protected virtual void TriggerDialogue()
    {
        if (!string.Equals(id, dialogueSet.id))
            return;

        dialogueManager.DisplayNextSentence();
    }

    public virtual void StartDialogue(string id)
    {
        if (string.IsNullOrEmpty(id))
            id = this.id;

        gameObject.SetActive(true);
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(dialogueSet);
    }
}
