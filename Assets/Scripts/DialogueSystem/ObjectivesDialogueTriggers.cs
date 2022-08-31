using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScriptablesInfo
{
    public string id;
    public DialogueScriptable dialogueScriptable;
}

public class ObjectivesDialogueTriggers : DialogueTrigger
{
    [SerializeField] private List<ScriptablesInfo> dialogueScriptableLists;

    public override void StartDialogue(string id)
    {
        dialogueManager.gameObject.SetActive(true);
        DialogueScriptable ds = dialogueScriptableLists.Find(c => c.id == id).dialogueScriptable;
        base.id = id;
        base.dialogueSet = ds;
        dialogueManager.StartDialogue(ds);
    }

    protected override void onDialogueEnd()
    {
        base.onDialogueEnd();
        dialogueManager.gameObject.SetActive(false);
    }

    public bool IfObjectiveCompleted(string id)
    {
        DialogueScriptable ds = dialogueScriptableLists.Find(c => c.id == id).dialogueScriptable;

        return ds.isCompleted;
    }
}
