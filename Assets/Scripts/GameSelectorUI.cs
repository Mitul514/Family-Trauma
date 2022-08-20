using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectorUI : MonoBehaviour
{
    [SerializeField] private Button firstTest, secondTest, playBtn;
    [SerializeField] private GameObject pfFirstTest, pfSecondTest;
    private GameObject firstpf, secondpf;
    
    private void OnEnable()
    {
        firstTest.gameObject.SetActive(false);
        secondTest.gameObject.SetActive(false);

        playBtn.onClick.AddListener(onPlayClicked);
        firstTest.onClick.AddListener(ShowFirstTest);
        secondTest.onClick.AddListener(ShowSecondTest);
    }

    private void OnDisable()
    {
        playBtn.onClick.RemoveListener(onPlayClicked);
        firstTest.onClick.RemoveListener(ShowFirstTest);
        secondTest.onClick.RemoveListener(ShowSecondTest);
    }

    private void onPlayClicked()
    {
        firstTest.gameObject.SetActive(true);
        secondTest.gameObject.SetActive(true);
        playBtn.gameObject.SetActive(false);
        ShowFirstTest();
    }

    private void ShowFirstTest() 
    {
        UnloadGO();
        firstpf = Instantiate(pfFirstTest);
        firstTest.interactable = false;
        secondTest.interactable = true;
    }

    private void ShowSecondTest()
    {
        UnloadGO();
        secondpf = Instantiate(pfSecondTest);
        firstTest.interactable = true;
        secondTest.interactable = false;
    }

    private void UnloadGO()
    {
        if (secondpf != null)
            Destroy(secondpf);

        if (firstpf != null)
            Destroy(firstpf);
    }
}
