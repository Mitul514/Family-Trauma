using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwoController : MonoBehaviour
{
    [SerializeField] private string objectiveId, narrativeId;
    [SerializeField] private DialogueTrigger phaseThreeTrigger;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private GameHudController hudController;

    private void OnEnable()
    {
        sceneTransition.dayChangeCompleted += OnDayChangeCompleted;
        phaseThreeTrigger.OnTriggereDialogueEnd += OnDialogueEnded;

        if (Phase3Ready())
        {
            sceneTransition.StartDayChangeTransition();
        }
    }

    private void OnDisable()
    {
        sceneTransition.dayChangeCompleted -= OnDayChangeCompleted;
        phaseThreeTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;
    }

    private void OnDialogueEnded(DialogueTrigger obj)
    {
        PlayerPrefController.Instance.UpdateNarrativeList(obj.dialogueId);

        hudController.SetSliderValue(90, obj.dialogueId);
    }

    private void OnDayChangeCompleted()
    {
        phaseThreeTrigger.StartDialogue("");
    }

    private bool Phase3Ready()
    {
        if (PlayerPrefController.Instance.ObjectiveIdLists.Contains(objectiveId) &&
            PlayerPrefController.Instance.NarrativeIdLists.Contains(narrativeId) &&
            !PlayerPrefController.Instance.NarrativeIdLists.Contains(phaseThreeTrigger.dialogueId))
        {
            return true;
        }
        return false;
    }
}
