using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollow : MonoBehaviour
{
    NavMeshAgent agent;

    GameObject parentObj;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        parentObj = transform.parent.gameObject;
    }

    private void Update()
    {
        agent.destination = parentObj.transform.position;
    }
}
