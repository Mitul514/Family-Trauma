using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectorUI : MonoBehaviour
{
    [SerializeField] private Button playBtn, exitBtn;
    [SerializeField] private BoxManager boxManagerPf;
    [SerializeField] private BoxesGameHudController hudController;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private string sceneToGo = "02_GameplayScene";

    private BoxManager firstpf;

    private void OnEnable()
    {
        playBtn.onClick.AddListener(onPlayClicked);
        exitBtn.onClick.AddListener(OnExitMiniGame);
    }

    private void OnDisable()
    {
        exitBtn.onClick.RemoveListener(OnExitMiniGame);
        playBtn.onClick.RemoveListener(onPlayClicked);
    }

    private void OnExitMiniGame()
    {
        sceneTransition.StartSceneTransition(sceneToGo);
    }

    private void onPlayClicked()
    {
        playBtn.gameObject.SetActive(false);
        ShowFirstTest();
    }

    private void ShowFirstTest()
    {
        firstpf = Instantiate(boxManagerPf);
        hudController.gameObject.SetActive(true);
        hudController.SetData(firstpf);
    }

    public void UnloadGO()
    {
        if (firstpf != null)
            Destroy(firstpf.gameObject);
        playBtn.gameObject.SetActive(true);
    }
}
