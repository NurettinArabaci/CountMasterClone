using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyArea : MonoBehaviour
{
    private int enemyCount;

    [SerializeField] private TextMeshProUGUI enemyText;

    [SerializeField] private Transform canvas;

    [SerializeField] private Animator circle;

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
            //EventManager.Fire_OnEnemyArea;
            Player.Instance.speed = 3;
            Player.Instance.xSpeed = 2f;
            mColl.enabled = false;
            ChildrenMove();



        }
    }

    

    public void AllEnemiesDied()
    {
        enemyCount--;
        enemyText.SetText(enemyCount.ToString());

        if (enemyCount <= 0)
        {
            EventManager.Fire_OnStartMovement();

            circle.SetBool(AnimConst.enemyFinish, true);

            Destroy(gameObject, 0.5f);

            Player.Instance.speed = 15;
            Player.Instance.xSpeed = 15f;

        }
    }

    void ChildrenMove()
    {
        foreach (var item in GetComponentsInChildren<Enemy>())
        {
            item.StartMove();
        }

    }

    void References()
    {
        mColl = GetComponent<BoxCollider>();
        enemyCount = transform.childCount - 1;
    }
}
