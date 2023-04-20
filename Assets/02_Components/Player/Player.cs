using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    Transform mT;
    Vector3 pos;

    public float speed = 15;
    public float xSpeed = 10f;
    public float limitX = 10f;

    private float mouseX;

    protected override void Awake()
    {
        base.Awake();
        mT = transform;
        pos = mT.position;
    }
    private void OnEnable()
    {
        EventManager.OnFinishArea += OnFinishArea;
    }

    private void OnDisable()
    {
        EventManager.OnFinishArea -= OnFinishArea;
    }

   
    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.Play && GameManager.Instance.gameState != GameState.Minigame) return;

        SwerveMovement();
    }

    void OnFinishArea()
    {
        xSpeed = 0;
        StartCoroutine(TweenMove());
    }

    void SwerveMovement()
    {
        
        pos += Vector3.forward*speed * Time.deltaTime;

        if (GameManager.Instance.gameState != GameState.Minigame)
        {
            if (Input.GetMouseButton(0))
            {


                mouseX = Input.GetAxis("Mouse X");

                pos += Vector3.right * mouseX * xSpeed * Time.deltaTime * 10;

            }

            pos = new Vector3(Mathf.Clamp(pos.x, -limitX, limitX), pos.y, pos.z);
            
        }
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

    
}
