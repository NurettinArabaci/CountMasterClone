using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DecreasePlayerAmount()
    {
        StackController.playerChildAmount--;
        PlayerCount.playerCount.text = StackController.playerChildAmount.ToString();

        if (StackController.playerChildAmount<=0)
        {
            EventManager.Fire_OnGameOver();
            EventManager.Fire_OnStopMovement();

        }
    }
}
