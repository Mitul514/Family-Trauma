using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button clearDataBtn;
	[SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private string sceneToGo = "01_GameplayScene";
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
		clearDataBtn.onClick.AddListener(OnClearDataBtnClicked);
	}

	private void OnDisable()
	{
		playBtn.onClick.RemoveListener(OnPlayBtnClicked);
		exitBtn.onClick.RemoveListener(OnExitBtnClicked);
		clearDataBtn.onClick.RemoveListener(OnClearDataBtnClicked);
	}

	private void OnClearDataBtnClicked()
	{
        PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
