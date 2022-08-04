using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [HideInInspector]public int enemyCount;

    [HideInInspector]public TextMeshProUGUI enemyText;

    [HideInInspector]public Animator circle;

    [HideInInspector] public Transform canvas;

    Collider mColl;

    private void Awake()
    {
        References();
    }
    private void Start()
    {
        enemyText.text = enemyCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag(Tags.PlayerChild))
        {
            Player.speed = 3;
            Player.xSpeed = 2f;
            mColl.enabled = false;
            
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            transform.GetChild(i).GetComponent<NavMeshAgent>().speed = 15;
            transform.GetChild(i).GetComponent<Animator>().SetBool(AnimConst.enemyRun, true);

        }
    }

    void References()
    {
        mColl = GetComponent<BoxCollider>();
        enemyCount = transform.childCount - 1;
        canvas = transform.GetChild(enemyCount);
        enemyText = canvas.GetChild(1).GetComponent<TextMeshProUGUI>();
        circle = canvas.GetChild(2).GetComponent<Animator>();
    }
}
