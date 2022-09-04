using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScriptablesInfo
{
    public string id;
    public DialogueScriptable dialogueScriptable;
    public EnititySO enitity;
}

public class ObjectivesDialogueTriggers : DialogueTrigger
{
    [SerializeField] private List<ScriptablesInfo> dialogueScriptableLists;

    public override void StartDialogue(string id, EnititySO enitity)
    {
        dialogueManager.gameObject.SetActive(true);
        ScriptablesInfo ds = dialogueScriptableLists.Find(c => c.id == id && c.enitity == enitity);
        base.id = ds.id;
        base.dialogueSet = ds.dialogueScriptable;
        dialogueManager.StartDialogue(ds.dialogueScriptable);
    }

    protected override void onDialogueEnd()
    {
        base.onDialogueEnd();
        dialogueManager.gameObject.SetActive(false);
    }
}
