using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class BoxesGameHudController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chances, mines;
    [SerializeField] private ChoicePopupUIController choicePopupUIController;
    [SerializeField] private Transform parent;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private GameSelectorUI gameSelectorUI;
    [SerializeField] private string sceneToGo = "02_GameplayScene";

    private ChoicePopupUIController choicePf;
    private BoxData m_boxData;
    private BoxManager m_boxManager;
    private int points = 0, chanceCount = 0;

    public void SetData(BoxManager boxManager)
    {
        UpdateUI(boxManager.SelectedBoxData);
        m_boxData = boxManager.SelectedBoxData;
        m_boxManager = boxManager;

        boxManager.OnChanceUse += OnChanceUsed;
        boxManager.OnMineFound += OnMineFounded;
        boxManager.OnWinOrLose += OnWinOrLoseUiUpdate;
    }

    private void OnBackClicked()
    {
        sceneTransition.StartSceneTransition(sceneToGo);
    }

    private void OnContinueClicked()
    {
        gameSelectorUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
        UpdateUI(m_boxManager.SelectedBoxData);

        Destroy(choicePf.gameObject, 0.5f);
    }

    private void OnWinOrLoseUiUpdate()
    {
        if (chanceCount >= m_boxData.totalChances && points < m_boxData.minesCount)
        {
            string popupTitle = "Sorry you failed!!!";
            string popupMsg = "Want to retry or go back?";
            ShowChoicePopup(popupTitle, popupMsg);
        }
        else if (chanceCount <= m_boxData.totalChances && points == m_boxData.minesCount)
        {
            string popupTitle = "Congratulation!!!";
            string popupMsg = "Want to Continue winning or go back?";
            ShowChoicePopup(popupTitle, popupMsg);
        }
    }

    private void ShowChoicePopup(string title, string msg)
    {
        choicePf = Instantiate(choicePopupUIController, parent);
        choicePf.popupTitle = title;
        choicePf.popupMsg = msg;
        gameSelectorUI.UnloadGO();
        choicePf.LoadPopupData();
        choicePf.OnContinueConfirmed += OnContinueClicked;
        choicePf.OnBackConfirm += OnBackClicked;
    }

    private void UpdateUI(BoxData boxData)
    {
        points = 0;
        chanceCount = 0;
        chances.text = $"Chances : {boxData.totalChances}/{boxData.totalChances}";
        mines.text = $"Mines : 0/{boxData.minesCount}";
    }

    private void OnChanceUsed(int val)
    {
        chanceCount++;
        val -= chanceCount;
        chances.text = $"Chance : {val}/{m_boxData.totalChances}";
    }

    private void OnMineFounded()
    {
        points++;
        mines.text = $"Mines : {points}/{m_boxData.minesCount}";
    }
}
