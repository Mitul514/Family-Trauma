using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoicePopupUIController : MonoBehaviour
{
    [SerializeField] private string popupTitle;
    [SerializeField, TextArea(5, 5)] private string popupMsg;
    [SerializeField] private Button continueBtn, backBtn;
    [SerializeField] private TextMeshProUGUI popupHeaderTxt, popupMsgTxt;

    public Action OnContinueConfirmed, OnBackConfirm;

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(OnConfirmClicked);
        backBtn.onClick.AddListener(OnBackClicked);

        LoadPopupData();
    }

    private void OnConfirmClicked()
    {
        OnContinueConfirmed?.Invoke();
    }

    private void OnBackClicked()
    {
        OnBackConfirm?.Invoke();
    }

    private void LoadPopupData()
    {
        popupHeaderTxt.text = popupTitle;
        popupMsgTxt.text = popupMsg;
    }
}
