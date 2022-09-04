using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherVisualController : MonoBehaviour
{
    [SerializeField] private string objectiveId;
    [SerializeField] private SceneTransition sceneTransition;

    private void OnEnable()
    {
        sceneTransition.sceneLoadCompleted += OnSceneLoadCompleted;
    }

    private void OnDisable()
    {
        sceneTransition.sceneLoadCompleted -= OnSceneLoadCompleted;
    }

    private void OnSceneLoadCompleted()
    {
        if (PlayerPrefController.Instance.ObjectiveIdLists.Contains(objectiveId))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
