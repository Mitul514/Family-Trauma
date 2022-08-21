using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float speed;
    [SerializeField] private EnititySO singleEntity, sceneChangeEntity, doorEntity;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private string sceneToGo = "02_GameplayScene";
    [SerializeField] private DialogueManager dialogueManager;

    private string sceneToGo1 = "01_GameplayScene";
    private float x, y;
    private bool stopMovement;

    private void OnEnable()
    {
        dialogueManager.onDialogueEnd += OnDialogueEnded;
    }

    private void OnDisable()
    {
        dialogueManager.onDialogueEnd -= OnDialogueEnded;
    }

    private void OnDialogueEnded()
    {
        dialogueManager.gameObject.SetActive(false);
        stopMovement = false;
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (stopMovement)
            return;
        transform.position += new Vector3(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EntityType enitity))
        {
            if (enitity.EnititySO == sceneChangeEntity)
            {
                sceneTransition.StartSceneTransition(sceneToGo);
            }
            else if (enitity.EnititySO == sceneChangeEntity)
            {
                sceneTransition.StartSceneTransition(sceneToGo1);
            }
            else if (enitity.EnititySO == singleEntity && enitity.TryGetComponent(out DialogueTrigger dialogueTrigger) &&
                !dialogueTrigger.isThisDialogueSetPlayed)
            {
                dialogueManager.gameObject.SetActive(true);
                stopMovement = true;
                dialogueTrigger.StartDialogue();
            }
        }

        if (collision.gameObject.TryGetComponent(out ShopController shopController))
        {
            shopController.InitShopping();
        }
    }
}
