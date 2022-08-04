using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform mT;
    Vector3 pos;

    Camera cam;

    public static float speed = 0;
    public static float xSpeed = 0f;
    public static float limitX = 10f;

    public static int playerChildCount = 1;

    public static bool forwardMove;

    float mouseX = 0;

    private void Awake()
    {
        mT = transform;
        pos = mT.position;
        cam = Camera.main;
    }
    private void OnEnable()
    {
        EventManager.StartMovement += Movement;
        EventManager.StopMovement += StopMove;
        EventManager.FinishArea += EnterFinishLine;
    }

    private void OnDisable()
    {
        EventManager.StartMovement -= Movement;
        EventManager.StopMovement -= StopMove;
        EventManager.FinishArea -= EnterFinishLine;
    }

    private void Start()
    {
        playerChildCount = 1;

        forwardMove = true;

        limitX = 10;

        EventManager.PlayerStop();
    }

    private void Update()
    {
        SwerveMovement();
    }

    private void LateUpdate()
    {
        if (forwardMove)
        {
            cam.transform.position = new Vector3(pos.x * 0.3f, pos.y + 40, pos.z - 45);
        }
        else cam.transform.position += new Vector3(0, speed * Time.deltaTime*0.8f, speed * Time.deltaTime);

    }

    void Movement()
    {
        speed = 15;
        xSpeed = 10; 
    }

    void StopMove()
    {
        speed = 0;
        xSpeed = 0; 
    }

    void EnterFinishLine()
    {
        xSpeed = 0;
        StartCoroutine(TweenMove());
        Invoke(nameof(BuildingStair), 0.5f);
    }

    void SwerveMovement()
    {
        
        pos += new Vector3(0, 0, speed * Time.deltaTime);
        
        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxis("Mouse X");
           
            pos += new Vector3(mouseX * xSpeed * Time.deltaTime * 10, 0, 0);
               
        }

        pos = new Vector3(Mathf.Clamp(pos.x, -limitX, limitX), pos.y, pos.z);
        transform.position = pos;
    }

    IEnumerator TweenMove()
    {
        float endTime=2f;
        float startTime = 0;

        while (startTime < endTime)
        {
            startTime += Time.deltaTime;
   
            pos = Vector3.Lerp(pos, new Vector3(0, pos.y, pos.z), startTime / endTime);

            yield return null;
        }
        

    }

    void BuildingStair()
    {
        GetComponent<BuildStair>().Build();
    }
    
}
