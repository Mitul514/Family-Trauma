using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private const string key = "FirstLaunch";
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private DialogueTrigger firstdialogueTrigger;
    [SerializeField] private List<GameObject> objectsToDisble;

    private void Start()
    {
        if (PlayerPrefs.GetInt(key) == 0)
        {
            EnableDisableObjects(false);
            firstdialogueTrigger.StartDialogue();
        }
    }

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
        sceneTransition.StartSceneTransition();
        EnableDisableObjects(true);
    }

    private void EnableDisableObjects(bool enable)
    {
        foreach (GameObject go in objectsToDisble)
        {
            go.SetActive(enable);
        }
    }
}
