using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEntityController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private ObjectivesDialogueTriggers objectiveDialogueTrigger;
    [SerializeField] private EnititySO playerEntity, objectiveEntity;
    [SerializeField] private string objectiveId, positiveId, negativeId;
    [SerializeField] private GameHudController gameHudController;
    [SerializeField] private int positiveVal, negativeVal;

    [Header("Triggers")]
    [SerializeField] private DialogueTrigger loopabledialogueTrigger;
    [SerializeField] private List<DialogueTrigger> dialogueTriggers;
    [SerializeField] private GameplayManager gameplayManager;

    private PlayerController player;
    private string[] ids;

    private void OnEnable()
    {
        ids = new string[dialogueTriggers.Count];

        for (int i = 0; i < dialogueTriggers.Count; i++)
        {
            ids[i] = dialogueTriggers[i].dialogueId;
            dialogueTriggers[i].OnTriggereDialogueEnd += OnDialogueEnded;
        }
        loopabledialogueTrigger.OnTriggereDialogueEnd += OnDialogueEnded;
    }

    private void OnDialogueEnded(DialogueTrigger dialogueTrigger)
    {
        float val = 0;
        loopabledialogueTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;

        if (dialogueTrigger.enititySO != GetComponent<EntityType>().EnititySO)
        {
            return;
        }

        dialogueManager.gameObject.SetActive(false);
        player.StopMovement = false;

        PlayerPrefController.Instance.UpdateNarrativeList(dialogueTrigger.dialogueId);

		#region Trust Meter
		if (dialogueTrigger.dialogueId == positiveId && !string.IsNullOrEmpty(positiveId))
        {
            val = positiveVal;
        }
        else if (dialogueTrigger.dialogueId == negativeId && !string.IsNullOrEmpty(positiveId))
        {
            val = negativeVal;
        }
        gameHudController.SetSliderValue(val, dialogueTrigger.dialogueId);
		#endregion

		StartCoroutine(PlayNarrationDialogue(dialogueTrigger));
        if (!PlayerPrefController.Instance.ObjectiveIdLists.Contains(objectiveId))
        {
            StartCoroutine(PlayObjectiveDialogue());
        }
    }

    private IEnumerator PlayObjectiveDialogue()
    {
        yield return new WaitForSeconds(3f);
        objectiveDialogueTrigger.gameObject.SetActive(true);
        objectiveDialogueTrigger.StartDialogue(objectiveId, objectiveEntity);
    }

    private IEnumerator PlayNarrationDialogue(DialogueTrigger dialogueTrigger)
    {
        yield return new WaitForSeconds(3f);
        gameplayManager.TriggerArcadeDialogue(dialogueTrigger.dialogueId);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent(out AnimatorController controller))
        {
            controller.StopAnimation();
        }

        if (collision.gameObject.TryGetComponent(out EntityType enitity))
        {
            if (enitity.EnititySO == playerEntity && enitity.TryGetComponent(out PlayerController player) &&
                TryGetComponent(out DialogueIdReciever idReciever))
            {
                this.player = player;

                if (idReciever.CanFetchTriggerID(dialogueTriggers, out DialogueTrigger trigger))
                {
                    dialogueManager.gameObject.SetActive(true);
                    player.StopMovement = true;
                    trigger.StartDialogue("");
                }
                else if (loopabledialogueTrigger.enititySO == GetComponent<EntityType>().EnititySO)
                {
                    dialogueManager.gameObject.SetActive(true);
                    player.StopMovement = true;
                    loopabledialogueTrigger.StartDialogue("");
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (TryGetComponent(out AnimatorController controller))
        {
            controller.PlayAnimation();
        }
    }
}
