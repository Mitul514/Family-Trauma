using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwoController : MonoBehaviour
{
    [SerializeField] private string objectiveId, narrativeId;
    [SerializeField] private string phaseEndId, finalNarrationId;
    [SerializeField] private DialogueTrigger phaseThreeBeginTrigger, phaseThreeEndTrigger, addictionTrigger;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private GameHudController hudController;
    [SerializeField] private PlayerController playerController;

    private void OnEnable()
    {
        sceneTransition.dayChangeCompleted += OnDayChangeCompleted;
        sceneTransition.sceneLoadCompleted += AddictionEndShow;
        phaseThreeBeginTrigger.OnTriggereDialogueEnd += OnDialogueEnded;
        phaseThreeEndTrigger.OnTriggereDialogueEnd += OnDialogueEnded;
        addictionTrigger.OnTriggereDialogueEnd += OnDialogueEnded;

        if (Phase3Ready())
        {
            StartCoroutine(StartDayCahngeCoroutine());
        }

        ShowPhase3End();
    }

    private void OnDisable()
    {
        sceneTransition.dayChangeCompleted -= OnDayChangeCompleted;
        phaseThreeBeginTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;
        phaseThreeEndTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;
        addictionTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;
        sceneTransition.sceneLoadCompleted -= AddictionEndShow;
    }

    private IEnumerator StartDayCahngeCoroutine()
    {
        yield return new WaitUntil(() => sceneTransition.IsSceneLoaded);
        playerController.StopMovement = true;
        sceneTransition.StartDayChangeTransition();
    }

    private void OnDialogueEnded(DialogueTrigger obj)
    {
        PlayerPrefController.Instance.UpdateNarrativeList(obj.dialogueId);
        playerController.StopMovement = false;
        if (obj.dialogueId == phaseThreeEndTrigger.dialogueId)
            hudController.SetSliderValue(90, obj.dialogueId);
    }

    private void OnDayChangeCompleted()
    {
        phaseThreeBeginTrigger.StartDialogue("");
    }

    private bool Phase3Ready()
    {
        if (PlayerPrefController.Instance.ObjectiveIdLists.Contains(objectiveId) &&
            PlayerPrefController.Instance.NarrativeIdLists.Contains(narrativeId) &&
            !PlayerPrefController.Instance.NarrativeIdLists.Contains(phaseThreeBeginTrigger.dialogueId))
        {
            return true;
        }
        return false;
    }

    private void ShowPhase3End()
    {
        if (PlayerPrefController.Instance.NarrativeIdLists.Contains(phaseEndId) &&
            !PlayerPrefController.Instance.NarrativeIdLists.Contains(phaseThreeEndTrigger.dialogueId))
        {
            playerController.StopMovement = true;
            phaseThreeEndTrigger.StartDialogue("");
        }
    }

    private void AddictionEndShow()
    {
        if (PlayerPrefController.Instance.NarrativeIdLists.Contains(finalNarrationId) &&
            !PlayerPrefController.Instance.NarrativeIdLists.Contains(addictionTrigger.dialogueId))
        {
            playerController.StopMovement = true;
            addictionTrigger.StartDialogue("");
        }
    }
}
