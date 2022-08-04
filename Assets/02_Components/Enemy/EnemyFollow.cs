using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    NavMeshAgent enemyNav;

    GameObject player;

    private void Start()
    {
        enemyNav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    void Update()
    {
        if (player != null)
        {
            enemyNav.destination = player.transform.position;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag(Tags.Player);
            return;
        }
    }

   
}