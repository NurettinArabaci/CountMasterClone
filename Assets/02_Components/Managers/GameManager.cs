using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Begin,
    Play,
    Minigame,
    GameEnd,
    Win,
    Lose
}

public static partial class GameStateEvent
{
    public static event System.Action<GameState> OnChangeGameState;
    public static void Fire_OnChangeGameState(GameState gameState) { OnChangeGameState?.Invoke(gameState); }
}


public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;

    protected override void Awake()
    {
        base.Awake();
        GameStateEvent.OnChangeGameState += OnChangeGameState;
    }

    private void Start()
    {
        OnChangeGameState(GameState.Begin);
    }

    void OnChangeGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.Begin:
                HandleBegin();
                break;

            case GameState.Play:
                HandlePlay();
                break;

            case GameState.Minigame:
                HandleMinigame();
                break;

            case GameState.GameEnd:
                HandleMinigame();
                break;

            case GameState.Win:
                HandleWin();
                break;

            case GameState.Lose:
                HandleLose();
                break;

            default:
                break;
        }
    }



    public void HandleBegin()
    {
        EventManager.Fire_OnBeginGame();

    }

    public void HandlePlay()
    {
        EventManager.Fire_OnPlayGame();
        EventManager.Fire_OnStartMovement();
    }

    public void HandleGameEnd()
    {

    }

    public void HandleMinigame()
    {
        EventManager.Fire_OnMiniGame();
    }

    public void HandleWin()
    {
        EventManager.Fire_OnWin();
        //EventManager.Fire_OnConfettiPlay();
    }

    public void HandleLose()
    {
        EventManager.Fire_OnLose();
    }

    public void DecreasePlayerAmount()
    {
        StackController.playerChildAmount--;
        PlayerCount.playerCount.text = StackController.playerChildAmount.ToString();

        if (StackController.playerChildAmount <= 0)
        {
            OnChangeGameState(GameState.Lose);
            //EventManager.Fire_OnStopMovement();

        }
    }

    private void OnDisable()
    {
        GameStateEvent.OnChangeGameState -= OnChangeGameState;
    }
}