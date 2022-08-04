using UnityEngine;

public class GroundControl : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerChild))
        {
            Player.limitX =Mathf.Abs( other.transform.parent.transform.position.x);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.PlayerChild))
        {
            Player.limitX = 10;
        }
    }
}
