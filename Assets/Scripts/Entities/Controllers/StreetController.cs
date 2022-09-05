using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetController : MonoBehaviour
{
    [SerializeField] private Arcadecontroller arcade1, arcade2;
    [SerializeField] private ShopController shop1, shop2;
    [SerializeField] private string objectiveId1, objectiveId2, objectiveId3, narrativeId1, narrativeId2, narrativeId3;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private DialogueTrigger trigger;

    private void OnEnable()
    {
        sceneTransition.sceneLoadCompleted += OnSceneLoadCompleted;
        trigger.OnTriggereDialogueEnd += OnDialogueEnded;

        StartArcadeObjective();
    }

    private void OnDisable()
    {
        sceneTransition.sceneLoadCompleted -= OnSceneLoadCompleted;
        trigger.OnTriggereDialogueEnd -= OnDialogueEnded;
    }

    private void OnDialogueEnded(DialogueTrigger obj)
    {
        PlayerPrefController.Instance.UpdateObjectiveList(obj.dialogueId);
    }

    private void StartArcadeObjective()
    {
        if (PlayerPrefController.Instance.NarrativeIdLists.Contains(narrativeId3) &&
            !PlayerPrefController.Instance.ObjectiveIdLists.Contains(objectiveId3))
        {
            trigger.StartDialogue("");
        }
    }

    private void OnSceneLoadCompleted()
    {
        if (PlayerPrefController.Instance.ObjectiveIdLists.Contains(objectiveId2) &&
            PlayerPrefController.Instance.NarrativeIdLists.Contains(narrativeId2))
        {
            arcade1.SetLock(false);
            arcade2.SetLock(false);
        }
        else
        {
            arcade1.SetLock(true);
            arcade2.SetLock(true);
        }

        if (PlayerPrefController.Instance.ObjectiveIdLists.Contains(objectiveId1) &&
            PlayerPrefController.Instance.NarrativeIdLists.Contains(narrativeId1))
        {
            shop2.SetLock(false);
        }
        else
        {
            shop2.SetLock(true);
        }
    }
}
