using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private SceneTransition sceneTransition;
    private string sceneToGo = "00_GameplayScene";
    private const string key = "FirstLaunch";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }

    private void OnEnable()
    {
        playBtn.onClick.AddListener(OnPlayBtnClicked);
        exitBtn.onClick.AddListener(OnExitBtnClicked);
    }

    private void OnExitBtnClicked()
    {
        Application.Quit();
    }

    private void OnPlayBtnClicked()
    {
        sceneTransition.StartSceneTransition(sceneToGo);
    }
}
