using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerChild))
        {
            StartCoroutine(DeathPlayer(other.gameObject));

            other.GetComponent<CapsuleCollider>().enabled = false;
            other.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            other.GetComponent<ParticleSystem>().Play(true);
 
            

        }
    }

    IEnumerator DeathPlayer(GameObject go)
    {
        GameManager.Instance.DecreasePlayerAmount();
        yield return new WaitForSecondsRealtime(0.5f);
        
        ObjectPooling.Instance.BackToPool(go, Tags.PlayerChild);
    }
}
