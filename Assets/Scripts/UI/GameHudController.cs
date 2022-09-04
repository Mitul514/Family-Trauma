using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHudController : MonoBehaviour
{
    [SerializeField] private TimerController timerController;
    [SerializeField] private TrustMeterController trustMeterController;
    [SerializeField] private float testValue, speed;
    private float timeScale = 0;
    private Coroutine coroutine;

    [SerializeField] private List<string> idList;

    public void SetData(bool isTimerOn)
    {
        if (isTimerOn)
        {
            timerController.gameObject.SetActive(true);
        }
        else
        {
            trustMeterController.gameObject.SetActive(true);
        }
    }

    public void RemoveHud()
    {
        Destroy(this);
    }

    [ContextMenu("Test")]
    public void TestSlider()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        SetSliderValue(testValue, "");
    }

    public void SetSliderValue(float value, string id)
    {
        if (!idList.Contains(id))
            return;

        if (PlayerPrefController.Instance.NarrativeIdLists.Contains(id) && value > 0)
        {
            timeScale = 0;
            coroutine = StartCoroutine(MoveSlider(value));
        }
    }

    private IEnumerator MoveSlider(float value)
    {
        float startValue = trustMeterController.slider.value;

        while (timeScale < 1)
        {
            timeScale += Time.deltaTime * speed;
            trustMeterController.slider.value = Mathf.Lerp(startValue, value, timeScale);
            yield return null;
        }
    }
}
