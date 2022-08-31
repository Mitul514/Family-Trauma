using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEntityController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private ObjectivesDialogueTriggers objectiveDialogueTrigger;
    [SerializeField] private EnititySO playerEntity;
    [SerializeField] private string objectiveId;
    [Header("Triggers")]
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private DialogueTrigger dialogueTrigger2;
    private PlayerController player;

    private void OnEnable()
    {
        if (!dialogueTrigger.isThisDialogueSetPlayed)
            dialogueTrigger.OnTriggereDialogueEnd += OnDialogueEnded;
        if (!dialogueTrigger2.isThisDialogueSetPlayed)
            dialogueTrigger2.OnTriggereDialogueEnd += OnDialogueEnded;
    }

    private void OnDialogueEnded()
    {
        dialogueTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;
        dialogueTrigger2.OnTriggereDialogueEnd -= OnDialogueEnded;

        //dialogueTrigger.gameObject.SetActive(false);
        //dialogueTrigger2.gameObject.SetActive(false);
        dialogueManager.gameObject.SetActive(false);
        player.StopMovement = false;

        StartCoroutine(PlayObjectDialogue());
    }

    private IEnumerator PlayObjectDialogue()
    {
        yield return new WaitForSeconds(3f);
        objectiveDialogueTrigger.gameObject.SetActive(true);
        objectiveDialogueTrigger.StartDialogue(objectiveId);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityType enitity))
        {
            if (enitity.EnititySO == playerEntity && enitity.TryGetComponent(out PlayerController player))
            {
                this.player = player;
                if (!dialogueTrigger.isThisDialogueSetPlayed)
                {
                    //dialogueTrigger.gameObject.SetActive(true);
                    dialogueManager.gameObject.SetActive(true);
                    player.StopMovement = true;
                    dialogueTrigger.StartDialogue("");
                }
                else if (objectiveDialogueTrigger.IfObjectiveCompleted(objectiveId))
                {
                    //dialogueTrigger2.gameObject.SetActive(true);
                    dialogueManager.gameObject.SetActive(true);
                    player.StopMovement = true;
                    dialogueTrigger2.StartDialogue("");
                }
            }
        }
    }
}
