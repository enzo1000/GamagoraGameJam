using UnityEngine;

public class TraceCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Ouais la télé");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ouais la télé2");
    }
}
