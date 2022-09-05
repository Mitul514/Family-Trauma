using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] protected DialogueManager dialogueManager;
    [SerializeField] protected SceneTransition sceneTransition;
    [SerializeField] protected List<GameObject> objectsToDisbleOnDialogueOn;
    [SerializeField] protected string narrationId;

    [Header("DialogueTriggers")]
    [SerializeField] private DialogueTrigger firstdialogueTrigger;
    [SerializeField] private DialogueTrigger dialogueTrigger1;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(KeyConstants.FIRSTLAUNCH_key))
        {
            PlayerPrefs.SetInt(KeyConstants.FIRSTLAUNCH_key, 0);
        }

        if (PlayerPrefs.GetInt(KeyConstants.FIRSTLAUNCH_key) == 0)
        {
            dialogueManager.gameObject.SetActive(true);
            EnableDisableObjects(false);
            firstdialogueTrigger.StartDialogue("");
        }
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt(KeyConstants.FIRSTLAUNCH_key) == 0)
            firstdialogueTrigger.OnTriggereDialogueEnd += OnDialogueEnded;

        dialogueTrigger1.OnTriggereDialogueEnd += OnNarrationDialogueEnded;
    }

    private void OnNarrationDialogueEnded(DialogueTrigger trigger)
    {
        dialogueTrigger1.OnTriggereDialogueEnd -= OnNarrationDialogueEnded;

        PlayerPrefController.Instance.UpdateNarrativeList(trigger.dialogueId);
    }

    private void OnDialogueEnded(DialogueTrigger trigger)
    {
        firstdialogueTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;

        PlayerPrefs.SetInt(KeyConstants.FIRSTLAUNCH_key, 1);
        EnableDisableObjects(true);
    }

    private void EnableDisableObjects(bool enable)
    {
        foreach (GameObject go in objectsToDisbleOnDialogueOn)
        {
            go.SetActive(enable);
        }
    }

    /// <summary>
	/// To trigger N2 narration
	/// </summary>
	/// <param name="id"></param>
    public void TriggerArcadeDialogue(string id)  
    {
        if (PlayerPrefController.Instance.NarrativeIdLists.Contains(id) && id == narrationId)
        {
            dialogueTrigger1.StartDialogue("");
        }
    }
}
