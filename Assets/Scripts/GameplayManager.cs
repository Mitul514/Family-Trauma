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
    [SerializeField] private List<GameObject> objectsToDisbleOnDialogueOn;
    [SerializeField] private GameObject dialoguePanel;

    private void Start()
    {
        if (PlayerPrefs.GetInt(key) == 0)
        {
            PlayerPrefs.SetInt(key, 1);
            dialoguePanel.SetActive(true);
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
        foreach (GameObject go in objectsToDisbleOnDialogueOn)
        {
            go.SetActive(enable);
        }
    }
}
