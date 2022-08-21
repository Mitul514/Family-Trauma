using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectorUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private GameObject pfFirstTest, pfSecondTest;
    private GameObject firstpf, secondpf;

    private void OnEnable()
    {
        playBtn.onClick.AddListener(onPlayClicked);
    }

    private void OnDisable()
    {
        playBtn.onClick.RemoveListener(onPlayClicked);
    }

    private void onPlayClicked()
    {
        playBtn.gameObject.SetActive(false);
        ShowFirstTest();
    }

    private void ShowFirstTest()
    {
        UnloadGO();
        firstpf = Instantiate(pfFirstTest);
    }

    private void ShowSecondTest()
    {
        UnloadGO();
        secondpf = Instantiate(pfSecondTest);
    }

    private void UnloadGO()
    {
        if (secondpf != null)
            Destroy(secondpf);

        if (firstpf != null)
            Destroy(firstpf);
    }
}
