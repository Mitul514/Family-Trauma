using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private float delay, dayChangeDelay;
    [SerializeField] private Animator animator;

    public Action sceneLoadCompleted, dayChangeCompleted;

    private void Start()
    {
        StartCoroutine(OnSceneUpdate());
    }

    private IEnumerator OnSceneUpdate()
    {
        animator.SetTrigger("Enter");
        yield return new WaitForSeconds(delay);
        sceneLoadCompleted?.Invoke();
        gameObject.SetActive(false);
    }

    #region Scene Transition
    public void StartSceneTransition(string sceneName = "")
    {
        gameObject.SetActive(true);
        StartCoroutine(OnSceneTransition(sceneName));
    }

    private IEnumerator OnSceneTransition(string sceneName)
    {
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(delay);

        if (TryGetComponent(out CameraAssigner assigner))
            assigner.AssignCamera();
        if (!SceneManager.GetActiveScene().Equals(sceneName) && !string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);

        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    #endregion

    #region DayChange Transition
    public void StartDayChangeTransition()
    {
        gameObject.SetActive(true);
        StartCoroutine(OnDayChangeTransition());
    }

    private IEnumerator OnDayChangeTransition()
    {
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(dayChangeDelay);
        animator.SetTrigger("Enter");
        dayChangeCompleted?.Invoke();
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    #endregion
}
