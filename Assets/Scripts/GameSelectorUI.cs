using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectorUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private BoxManager boxManagerPf;
    [SerializeField] private BoxesGameHudController hudController;

    private BoxManager firstpf;

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
        firstpf = Instantiate(boxManagerPf);
        hudController.gameObject.SetActive(true);
        hudController.SetData(firstpf);
    }

    public void UnloadGO()
    {
        if (firstpf != null)
            Destroy(firstpf.gameObject);
        playBtn.gameObject.SetActive(true);
    }
}
