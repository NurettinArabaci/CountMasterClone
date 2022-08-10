
using UnityEngine;

public class PlayerChild : MonoBehaviour
{
    Animator anim;

    [HideInInspector]public ParticleSystem dieFx;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        dieFx = GetComponent<ParticleSystem>();

    }
    
    private void OnEnable()
    {
        dieFx.Pause(true);
        EventManager.OnStartMovement += MovementChild;
        EventManager.OnStopMovement += StopMoveChild;
    }

    private void OnDisable()
    {
        EventManager.OnStartMovement -= MovementChild;
        EventManager.OnStopMovement -= StopMoveChild;
    }

    void MovementChild()
    {
        anim.SetBool(AnimConst.isRun, true);

    }

    void StopMoveChild()
    {
        anim.SetBool(AnimConst.isRun, false);
    }

}
