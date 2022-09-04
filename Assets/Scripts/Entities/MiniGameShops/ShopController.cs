using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private bool isInitialized;
    [SerializeField] private bool isLocked;
    [SerializeField] private EnititySO playerEntity;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private DialogueScriptable objectiveSO;

    private PlayerController player;

    public bool DialogueEnd { get; private set; }

    private void OnEnable()
    {
        dialogueTrigger.OnTriggereDialogueEnd += OnDialogueEnd;
    }

    private void OnDisable()
    {
        dialogueTrigger.OnTriggereDialogueEnd -= OnDialogueEnd;
    }

    private void OnDialogueEnd(DialogueTrigger trigger)
    {
        if (player != null)
        {
            player.StopMovement = false;
            PlayerPrefController.Instance.UpdateObjectiveList(objectiveSO.id);
        }
    }

    public void SetLock(bool enable)
    {
        isLocked = enable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityType enitity) && !isLocked)
        {
            player = enitity.GetComponent<PlayerController>();
            if (enitity.EnititySO == playerEntity)
            {
                PlayShopDialogue();
            }
        }
    }

    private void PlayShopDialogue()
    {
        if (player != null)
        {
            player.StopMovement = true;
        }
        DialogueEnd = false;
        dialogueTrigger.StartDialogue("");
    }
}
