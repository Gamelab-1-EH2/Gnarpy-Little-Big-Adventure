using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTrigger;
    [SerializeField] private LayerMask _triggerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _triggerMask)
            _onTrigger?.Invoke();
    }

    private void OnDestroy()
    {
        _onTrigger.RemoveAllListeners();
    }
}
