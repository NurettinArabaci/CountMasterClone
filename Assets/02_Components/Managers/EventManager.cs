using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class EventManager
{
    public static event Action OnStartMovement;
    public static void Fire_OnStartMovement(){ OnStartMovement?.Invoke(); }

    public static event Action OnStopMovement;
    public static void Fire_OnStopMovement() { OnStopMovement?.Invoke(); }

    public static event Action OnFinishArea;
    public static void Fire_OnFinishArea() { OnFinishArea?.Invoke(); }

    public static event Action OnGameOver;
    public static void Fire_OnGameOver() { OnGameOver?.Invoke(); }

    public static event Action LevelCompleted;
    public static void NextLevel() { LevelCompleted?.Invoke(); }

}
