using System;
using UnityEngine;

/// <summary>
/// Defines the <see cref="ConnectingDotsManager" />.
/// </summary>
public class ConnectingDotsManager : MonoBehaviour
{
    public static ConnectingDotsManager Instance { get; set; }

    #region Variables

    public event Action MenuTrigger;

    public event Action PauseTrigger;

    public event Action GameOverTrigger;

    public event Action<string> GameTrigger;

    public event Action PrevLevelDestroyTrigger;

    private int _count = 1;

    public GameStates States = GameStates.None;

    [HideInInspector]
    public bool DestroyPrevLevel;

    [HideInInspector] public int MoveCount;

    [HideInInspector]
    public int TotalMoveCount = 10;

    [HideInInspector]
    public int FlowMatchedCount = 0;

    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MoveCount = 10;
    }

    private void Update()
    {
        if (_count > 0)
        {
            SetState(GameStates.MenuState);
            _count -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && States == GameStates.GamePlayState)
            SetState(GameStates.PauseState);
        if (DestroyPrevLevel)
        {
            PrevLevelDestroyTrigger?.Invoke();
            DestroyPrevLevel = false;
        }
    }
        
    public void SetState(GameStates states, string level = "")
    {
        switch (states)
        {
            case GameStates.MenuState:
                MenuTrigger?.Invoke();
                break;
            case GameStates.GamePlayState:
                GameTrigger?.Invoke(level);
                break;
            case GameStates.PauseState:
                PauseTrigger?.Invoke();
                break;
            case GameStates.GameOverState:
                GameOverTrigger?.Invoke();
                break;
            case GameStates.None:
                break;
            default:
                break;
        }

        States = states;
    }
}
