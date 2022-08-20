using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private Button continueBtn;
    [SerializeField] private DialogueScriptable dialogueSet;
    [SerializeField, Tooltip("To test set key and play")] private KeyCode keyCode;

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(TriggerDialogue);
    }

    private void OnDisable()
    {
        continueBtn?.onClick.RemoveListener(TriggerDialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
            DialogueManager.Instance.StartDialogue(dialogueSet.dialogue);
    }

    private void TriggerDialogue()
    {
        if (!string.Equals(id, dialogueSet.id))
            return;

        DialogueManager.Instance.DisplayNextSentence();
    }

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueSet.dialogue);
    }
}
