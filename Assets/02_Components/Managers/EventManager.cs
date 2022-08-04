using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class EventManager
{
    public static event Action StartMovement;
    public static void PlayerMove(){ StartMovement?.Invoke(); }

    public static event Action StopMovement;
    public static void PlayerStop() { StopMovement?.Invoke(); }

    public static event Action FinishArea;
    public static void EnterFinishArea() { FinishArea?.Invoke(); }

    public static event Action GameOver;
    public static void OnGameOver() { GameOver?.Invoke(); }

    public static event Action LevelCompleted;
    public static void NextLevel() { LevelCompleted?.Invoke(); }

}
