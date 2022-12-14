using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject parent;
    EnemyArea enemyScript;

    Collider enemyColl;

    SkinnedMeshRenderer mesh;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        enemyScript = parent.GetComponent<EnemyArea>();
        enemyColl = GetComponent<CapsuleCollider>();
        mesh= transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerChild))
        {
            enemyScript.enemyCount--;

            other.GetComponent<CapsuleCollider>().enabled = false;
            other.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            other.GetComponent<ParticleSystem>().Play(true);

            GameManager.Instance.DecreasePlayerAmount();

            StartCoroutine(DeathPlayers(other.gameObject));
           
            enemyScript.enemyText.text = enemyScript.enemyCount.ToString();

            AllEnemiesDied();
            enemyColl.enabled = false;
            mesh.enabled = false;

        }
    }

    void AllEnemiesDied()
    {
        if (enemyScript.enemyCount <= 0)
        {
            EventManager.Fire_OnStartMovement();

            enemyScript.circle.SetBool(AnimConst.enemyFinish, true);

            Destroy(parent.gameObject, 0.5f);

            Player.Instance.speed = 15;
            Player.Instance.xSpeed = 15f;

        }
    }
   

    IEnumerator DeathPlayers(GameObject go)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ObjectPooling.Instance.BackToPool(go, Tags.PlayerChild);
        gameObject.SetActive(false);
    }
  
}
