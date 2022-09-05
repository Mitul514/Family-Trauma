using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private float delay, dayChangeDelay;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject textObject;

    public Action sceneLoadCompleted, dayChangeCompleted;
    public bool IsSceneLoaded { get; private set; }

    private void Start()
    {
        StartCoroutine(OnSceneUpdate());
    }

    private IEnumerator OnSceneUpdate()
    {
        animator.SetTrigger("Enter");
        sceneLoadCompleted?.Invoke();
        yield return new WaitForSeconds(delay);
        IsSceneLoaded = true;
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
        IsSceneLoaded = true;
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
        textObject.SetActive(true);
        yield return new WaitForSeconds(dayChangeDelay);
        animator.SetTrigger("Enter");
        yield return new WaitForSeconds(delay);
        dayChangeCompleted?.Invoke();
        textObject.SetActive(false);
        gameObject.SetActive(false);
    }
    #endregion
}
