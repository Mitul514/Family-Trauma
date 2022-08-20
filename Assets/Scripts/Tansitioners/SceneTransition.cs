using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private Animator animator;

    private void Start()
    {
        StartCoroutine(OnSceneUpdate());
    }

    public void StartSceneTransition(string sceneName = "")
    {
        gameObject.SetActive(true);
        StartCoroutine(OnSceneTransition(sceneName));
    }

    private IEnumerator OnSceneUpdate()
    {
        animator.SetTrigger("Enter");
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private IEnumerator OnSceneTransition(string sceneName)
    {
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(delay);

        if (!SceneManager.GetActiveScene().Equals(sceneName) && !string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);

        if (TryGetComponent(out CameraAssigner assigner))
            assigner.AssignCamera();

        animator.SetTrigger("Enter");
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
