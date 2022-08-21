using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxesGameHudController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chances, mines;
    [SerializeField] private ChoicePopupUIController choicePopupUIController;
    [SerializeField] private Transform parent;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private GameSelectorUI gameSelectorUI;
    [SerializeField] private string sceneToGo = "02_GameplayScene";

    private BoxData m_boxData;
    private BoxManager m_boxManager;
    private int points = 0, chanceCount = 0;

    public void SetData(BoxManager boxManager)
    {
        UpdateUI(boxManager.BoxData);
        m_boxData = boxManager.BoxData;
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
    }

    private void OnWinOrLoseUiUpdate()
    {
        if (chanceCount == m_boxData.totalChances && points != m_boxData.minesCount)
        {
            var go = Instantiate(choicePopupUIController, parent);
            go.popupTitle = "Sorry you failed!!!";
            go.popupMsg = "Want to retry or go back?";
            gameSelectorUI.UnloadGO();

            go.OnContinueConfirmed += OnContinueClicked;
            go.OnBackConfirm += OnBackClicked;
        }
        else if (chanceCount <= m_boxData.totalChances && points == m_boxData.minesCount)
        {
            var go = Instantiate(choicePopupUIController, parent);
            go.popupTitle = "Congratulation!!!";
            go.popupMsg = "Want to Continue winning or go back?";
            gameSelectorUI.UnloadGO();

            go.OnContinueConfirmed += OnContinueClicked;
            go.OnBackConfirm += OnBackClicked;
        }
    }

    private void UpdateUI(BoxData boxData)
    {
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
