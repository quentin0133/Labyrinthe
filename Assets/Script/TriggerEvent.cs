using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField]
    private LayerMask detectionLayer;
    [SerializeField]
    private UnityEvent collisionEvent;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (((1 << trigger.gameObject.layer) & detectionLayer) != 0)
        {
            collisionEvent.Invoke();
        }
    }
}
