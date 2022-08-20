using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
  [SerializeField] private Button _playButton;

  [SerializeField] private Button _exitButton;

  [SerializeField] private Button _exit2Button;

  [SerializeField] private Button _pauseButton;

  [SerializeField] private Button _levelButton;

  [SerializeField] private Button _gameOverLevelButton;

  [SerializeField] private Button _exit3Button;

  [SerializeField] private GameObject _menu;

  [SerializeField] private GameObject _pauseMenu;

  [SerializeField] private GameObject _gameOver;

  [SerializeField] private GameObject _levelBtnPrefab;

  [SerializeField] private GameObject _levelSpawner;

  [SerializeField] private GameObject _gameSpawner;

  [SerializeField] private GameObject _gameHUD;

  [SerializeField] private Text _moveLeftText;

  [SerializeField] private Text _matchCountText;

  [SerializeField] private Text _ConclusionHUDText;

  private int levelCount = 5;

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

  /// <summary>
  /// The ShowMenu.
  /// </summary>
  private void ShowMenu()
  {
    _gameOver.SetActive(false);
    _gameSpawner.SetActive(false);
    _menu.SetActive(true);
    _pauseMenu.SetActive(false);
    _levelSpawner.SetActive(false);
    _gameHUD.SetActive(false);
    _playButton.onClick.AddListener(OnPlayClicked);
    _exitButton.onClick.AddListener(OnExitClicked);
  }

  /// <summary>
  /// The PauseMenu.
  /// </summary>
  private void PauseMenu()
  {
    _gameOver.SetActive(false);
    _gameSpawner.SetActive(false);
    _menu.SetActive(false);
    _pauseMenu.SetActive(true);
    _levelSpawner.SetActive(false);
    _gameHUD.SetActive(false);
    _exit2Button.onClick.AddListener(OnExitClicked);
    _levelButton.onClick.AddListener(OnLevelClicked);
    _pauseButton.onClick.AddListener(OnResumeClicked);
  }

  /// <summary>
  /// The ShowGameOver.
  /// </summary>
  private void ShowGameOver()
  {
    _gameOver.SetActive(true);
    _gameSpawner.SetActive(false);
    _menu.SetActive(false);
    _pauseMenu.SetActive(false);
    _levelSpawner.SetActive(false);
    _gameHUD.SetActive(false);

    if (ConnectingDotsManager.Instance.MoveCount == 0)
    {
      _ConclusionHUDText.text = "GAMEOVER !!";
    }
    else if (ConnectingDotsManager.Instance.FlowMatchedCount == 5)
    {
      _ConclusionHUDText.text = "CONGRATULATION *v*";
    }
    _exit3Button.onClick.AddListener(OnExitClicked);
    _gameOverLevelButton.onClick.AddListener(OnLevelClicked);
  }

  /// <summary>
  /// The ShowGameHUD.
  /// </summary>
  /// <param name="obj">The obj<see cref="string"/>.</param>
  private void ShowGameHUD(string obj)
  {
    _gameHUD.SetActive(true);
  }

  /// <summary>
  /// The OnResumeClicked.
  /// </summary>
  private void OnResumeClicked()
  {
    _gameSpawner.SetActive(true);
    _pauseMenu.SetActive(false); ;
  }

  /// <summary>
  /// The OnLevelClicked.
  /// </summary>
  private void OnLevelClicked()
  {
    _levelSpawner.SetActive(true);
    _pauseMenu.SetActive(false);
    _gameOver.SetActive(false);
    ConnectingDotsManager.Instance.MoveCount = 10;
    ConnectingDotsManager.Instance.FlowMatchedCount = 0;
    ConnectingDotsManager.Instance.DestroyPrevLevel = true;
  }

  private void OnExitClicked()
  {
    Application.Quit();
  }

  /// <summary>
  /// The OnPlayClicked.
  /// </summary>
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
        _gameSpawner.SetActive(true);
        ConnectingDotsManager.Instance.SetState(GameStates.GamePlayState, _btn.name);
        _levelSpawner.SetActive(false);
      });
    }
  }
}
