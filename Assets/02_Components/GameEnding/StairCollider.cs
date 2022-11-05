using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Stair))
        {
            transform.parent = other.transform;
            BuildStair.stairId--;
            if (BuildStair.stairId<=0)
            {
                EventManager.Fire_OnStopMovement();
                GameStateEvent.Fire_OnChangeGameState(GameState.Win);
            }
            //Player.forwardMove = false;
        }
    }
}
