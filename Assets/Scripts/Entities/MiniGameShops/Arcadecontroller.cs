using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcadecontroller : MonoBehaviour
{
    [SerializeField] private bool isLocked = true;
    [SerializeField] private EnititySO playerEntity;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private string sceneToGo = "02_GameplayScene";
    private PlayerController player;

    private void OnEnable()
    {
        dialogueTrigger.OnTriggereDialogueEnd += ShowMiniGame;
    }

    private void OnDisable()
    {
        dialogueTrigger.OnTriggereDialogueEnd -= ShowMiniGame;
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
                PlayArcadeDialogue();
            }
        }
    }

    private void PlayArcadeDialogue()
    {
        if (player != null)
        {
            player.StopMovement = true;
        }
        dialogueTrigger.StartDialogue("");
    }

    private void ShowMiniGame()
    {
        if (player != null)
        {
            player.StopMovement = false;
        }
        sceneTransition.StartSceneTransition(sceneToGo);
    }
}
