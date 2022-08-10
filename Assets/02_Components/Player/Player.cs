using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

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
        instance = this;
        mT = transform;
        pos = mT.position;
        cam = Camera.main;
    }
    private void OnEnable()
    {
        EventManager.OnStartMovement += OnStartMovement;
        EventManager.OnStopMovement += OnStopMovement;
        EventManager.OnFinishArea += OnFinishArea;
    }

    private void OnDisable()
    {
        EventManager.OnStartMovement -= OnStartMovement;
        EventManager.OnStopMovement -= OnStopMovement;
        EventManager.OnFinishArea -= OnFinishArea;
    }

    private void Start()
    {
        playerChildCount = 1;

        forwardMove = true;

        limitX = 10;

        EventManager.Fire_OnStopMovement();
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

    void OnStartMovement()
    {
        speed = 15;
        xSpeed = 10; 
    }

    void OnStopMovement()
    {
        speed = 0;
        xSpeed = 0; 
    }

    void OnFinishArea()
    {
        xSpeed = 0;
        StartCoroutine(TweenMove());
        Invoke(nameof(BuildingStair), 0.5f);
    }

    void SwerveMovement()
    {
        
        pos += Vector3.forward*speed * Time.deltaTime;
        
        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxis("Mouse X");

            pos += Vector3.right * mouseX * xSpeed * Time.deltaTime * 10;
               
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
