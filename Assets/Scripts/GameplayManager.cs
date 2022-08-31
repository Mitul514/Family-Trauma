using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private const string key = "FirstLaunch";
    [SerializeField] protected DialogueManager dialogueManager;
    [SerializeField] protected SceneTransition sceneTransition;
    [SerializeField] protected List<GameObject> objectsToDisbleOnDialogueOn;

    [Header("DialogueTriggers")]
    [SerializeField] private DialogueTrigger firstdialogueTrigger;

    private void Start()
    {
        if (PlayerPrefs.GetInt(key) == 0)
        {
            PlayerPrefs.SetInt(key, 1);
            dialogueManager.gameObject.SetActive(true);
            EnableDisableObjects(false);
            firstdialogueTrigger.StartDialogue("");
            //firstdialogueTrigger.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        //firstdialogueTrigger.gameObject.SetActive(false);
        if (!firstdialogueTrigger.isThisDialogueSetPlayed)
            firstdialogueTrigger.OnTriggereDialogueEnd += OnDialogueEnded;
    }

    private void OnDialogueEnded()
    {
        firstdialogueTrigger.OnTriggereDialogueEnd -= OnDialogueEnded;
        EnableDisableObjects(true);
        //firstdialogueTrigger.gameObject.SetActive(false);
    }

    private void EnableDisableObjects(bool enable)
    {
        foreach (GameObject go in objectsToDisbleOnDialogueOn)
        {
            go.SetActive(enable);
        }
    }
}
