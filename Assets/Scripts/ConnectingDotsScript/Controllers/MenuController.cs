using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton, _exitButton, _exit2Button, _pauseButton;
    [SerializeField] private GameObject _menu, _pauseMenu, _levelBtnPrefab, _levelSpawner, _gameSpawner, _gameHUD;
    [SerializeField] private Text _moveLeftText, _matchCountText, _ConclusionHUDText;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private ChoicePopupUIController choicePopupUIController;
    [SerializeField] private Transform parent;
    [SerializeField] private string sceneToGo = "02_GameplayScene";

    private int levelCount = 5;
    private ChoicePopupUIController choicePf;

    private void Start()
    {
        ConnectingDotsManager.Instance.MenuTrigger += ShowMenu;
        ConnectingDotsManager.Instance.PauseTrigger += PauseMenu;
        ConnectingDotsManager.Instance.GameTrigger += ShowGameHUD;
        ConnectingDotsManager.Instance.GameOverTrigger += ShowGameOver;
    }

    private void Update()
    {
        _matchCountText.text = "Flow Matched : " + ConnectingDotsManager.Instance.FlowMatchedCount + " / 5";
        _moveLeftText.text = "Move Left : " + ConnectingDotsManager.Instance.MoveCount + " / " + ConnectingDotsManager.Instance.TotalMoveCount;
    }

    private void OnDestroy()
    {
        ConnectingDotsManager.Instance.MenuTrigger -= ShowMenu;
        ConnectingDotsManager.Instance.PauseTrigger -= PauseMenu;
    }

    private void ShowMenu()
    {
        _menu.SetActive(true);
        _pauseMenu.SetActive(false);
        _levelSpawner.SetActive(false);
        _gameHUD.SetActive(false);
        _playButton.onClick.AddListener(OnPlayClicked);
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    private void PauseMenu()
    {
        _menu.SetActive(false);
        _pauseMenu.SetActive(true);
        _levelSpawner.SetActive(false);
        _gameHUD.SetActive(false);
        _exit2Button.onClick.AddListener(OnExitClicked);
        _pauseButton.onClick.AddListener(OnResumeClicked);
    }

    private void ShowGameOver()
    {
        _menu.SetActive(false);
        _pauseMenu.SetActive(false);
        _levelSpawner.SetActive(false);
        _gameHUD.SetActive(false);

        OnWinOrLoseUiUpdate();
    }

    private void OnWinOrLoseUiUpdate()
    {
        if (ConnectingDotsManager.Instance.MoveCount == 0)
        {
            string popupTitle = "Sorry you failed!!!";
            string popupMsg = "Want to retry or go back?";
            ShowChoicePopup(popupTitle, popupMsg);
        }
        else if (ConnectingDotsManager.Instance.FlowMatchedCount == 5)
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
        choicePf.LoadPopupData();
        choicePf.OnContinueConfirmed += OnContinueClicked;
        choicePf.OnBackConfirm += OnBackClicked;
    }

    private void OnBackClicked()
    {
        sceneTransition.StartSceneTransition(sceneToGo);
    }

    private void OnContinueClicked()
    {
        ConnectingDotsManager.Instance.SetState(GameStates.MenuState);
        Destroy(choicePf.gameObject, 0.5f);
        _menu.SetActive(true);
    }

    private void ShowGameHUD(string obj)
    {
        _gameHUD.SetActive(true);
    }

    private void OnResumeClicked()
    {
        ConnectingDotsManager.Instance.SetState(GameStates.GamePlayState);
        _pauseMenu.SetActive(false);
    }

    private void OnExitClicked()
    {
        sceneTransition.StartSceneTransition(sceneToGo);
    }

    private void OnPlayClicked()
    {
        _menu.SetActive(false);
        _levelSpawner.SetActive(true);
        _pauseMenu.SetActive(false);
        for (int i = 0; i < levelCount; i++)
        {
            var _btnClone = Instantiate(_levelBtnPrefab, _levelSpawner.transform);
            _btnClone.name = "Level " + (i + 1);
            _btnClone.GetComponentInChildren<Text>().text = "Level " + (i + 1);

            var _btn = _btnClone.GetComponent<Button>();
            _btn.onClick.AddListener(() =>
            {
                ConnectingDotsManager.Instance.SetState(GameStates.GamePlayState, _btn.name);
                _levelSpawner.SetActive(false);
            });
        }
    }
}
