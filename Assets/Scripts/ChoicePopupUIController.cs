using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoicePopupUIController : MonoBehaviour
{
    [SerializeField] public string popupTitle;
    [SerializeField, TextArea(5, 5)] public string popupMsg;
    [SerializeField] private Button continueBtn, backBtn;
    [SerializeField] private TextMeshProUGUI popupHeaderTxt, popupMsgTxt;
    [SerializeField] private string narrationId;

    public Action OnContinueConfirmed, OnBackConfirm;

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(OnConfirmClicked);
        backBtn.onClick.AddListener(OnBackClicked);

        CheckForPhaseBegin();
    }

    private void CheckForPhaseBegin()
    {
        if (PlayerPrefController.Instance.NarrativeIdLists.Contains(narrationId))
        {
            backBtn.interactable = false;
        }
    }

    private void OnConfirmClicked()
    {
        OnContinueConfirmed?.Invoke();
    }

    private void OnBackClicked()
    {
        OnBackConfirm?.Invoke();
    }

    public void LoadPopupData()
    {
        popupHeaderTxt.text = popupTitle;
        popupMsgTxt.text = popupMsg;
    }
}
