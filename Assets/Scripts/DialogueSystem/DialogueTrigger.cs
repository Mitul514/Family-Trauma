using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected DialogueScriptable dialogueSet;
    [SerializeField] protected Button continueBtn;
    [SerializeField] protected DialogueManager dialogueManager;
    [SerializeField] protected EnititySO dialogueOfEntity;

    public Action<DialogueTrigger> OnTriggereDialogueEnd;
    public string dialogueId => dialogueSet.id;
    public EnititySO enititySO => dialogueOfEntity;

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
        OnTriggereDialogueEnd?.Invoke(this);
        gameObject.SetActive(false);
    }

    protected virtual void TriggerDialogue()
    {
        if (!string.Equals(id, dialogueSet.id))
            return;

        dialogueManager.DisplayNextSentence();
    }

    public virtual void StartDialogue(string id, EnititySO enitity = null)
    {
        if (string.IsNullOrEmpty(id))
            id = this.id;

        gameObject.SetActive(true);
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue(dialogueSet);
    }
}
