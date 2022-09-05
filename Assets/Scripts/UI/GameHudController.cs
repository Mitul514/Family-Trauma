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

	private void OnEnable()
	{
        SetData();
	}

	public void SetData()
    {
        var value = PlayerPrefController.Instance.GetMeterValue();
		trustMeterController.slider.value = value == 0 ? trustMeterController.slider.value : value;        
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
            PlayerPrefController.Instance.UpdateTrustMeterKey(value);
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
