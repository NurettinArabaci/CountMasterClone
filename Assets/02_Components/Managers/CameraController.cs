using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform followObject;
    private Transform mT;

    private void Awake()
    {
        mT = transform;
    }

    private void LateUpdate()
    {

        mT.position = new Vector3(followObject.position.x * 0.2f, 40, followObject.position.z - 45f);
    }
}
