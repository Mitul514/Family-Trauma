using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHudController : MonoBehaviour
{
    [SerializeField] private TimerController timerController;
    [SerializeField] private TrustMeterController trustMeterController;

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
}
