using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildStair : MonoBehaviour
{
    [SerializeField] int perRowMaxHumanCount;
    [SerializeField] float distanceBetweenHumans;
    List<int> stairCountList;
    List<GameObject> stairList;

    public static int stairId = 0;


    private void Start()
    {
        stairCountList = new List<int>();
        stairList = new List<GameObject>();
    }
    
    public void Build()
    {
        FillStairList();
        StartCoroutine(BuildStairCoroutine());
    }


    void FillStairList()
    {
        int playerChildCount = StackController.playerChildAmount;

        for (int i = 1; i <= perRowMaxHumanCount; i++)
        {
            if (playerChildCount < i)
            {
                break;
            }
            playerChildCount -= i;
            stairCountList.Add(i);
        }
        for (int i = perRowMaxHumanCount; i > 0; i--)
        {
            if (playerChildCount >= i)
            {
                playerChildCount -= i;
                stairCountList.Add(i);
                i++;
            }
        }
        stairCountList.Sort();
    }


    IEnumerator BuildStairCoroutine()
    {
        
        Vector3 sum;
        GameObject stairStep;

        float tempTowerStairCount;

        foreach (int inStairPlayerCount in stairCountList)
        {
            foreach (GameObject child in stairList)
            {
                child.transform.localPosition += Vector3.up*3;
            }

            stairStep = new GameObject("StairStep" + stairId);

            AddComponentAndTransform(stairStep);

            stairList.Add(stairStep);

            sum = Vector3.zero;
            tempTowerStairCount = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.CompareTag(Tags.PlayerChild))
                {
                    child.GetComponent<NavMeshAgent>().enabled = false;
                    child.GetComponent<NavMeshFollow>().enabled = false;

                    child.transform.parent = stairStep.transform;
                    child.transform.localPosition = Vector3.right * tempTowerStairCount * distanceBetweenHumans;
                    sum += child.transform.position;
                    tempTowerStairCount++;
                    i--;

                    if (tempTowerStairCount >= inStairPlayerCount)
                    {
                        break;
                    }
                }
            }

            stairStep.transform.position = new Vector3(-sum.x / inStairPlayerCount, stairStep.transform.position.y, stairStep.transform.position.z);
            sum = Vector3.zero;
            stairId++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void AddComponentAndTransform(GameObject go)
    {
        go.AddComponent<BoxCollider>().center = Vector3.up + Vector3.forward;
        go.GetComponent<BoxCollider>().isTrigger = true;
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.AddComponent<StairCollider>();
    }
}
