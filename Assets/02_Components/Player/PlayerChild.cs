
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
        EventManager.StartMovement += MovementChild;
        EventManager.StopMovement += StopMoveChild;
    }

    private void OnDisable()
    {
        EventManager.StartMovement -= MovementChild;
        EventManager.StopMovement -= StopMoveChild;
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
