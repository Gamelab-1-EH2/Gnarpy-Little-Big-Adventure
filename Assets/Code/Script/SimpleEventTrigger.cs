using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SimpleEventTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("Tag dell'oggetto da rilevare")]
    private string targetTag;

    [SerializeField, Tooltip("Permetti l'attivazione una sola volta")]
    private bool once = false;

    private bool enterDone = false;
    private bool stayDone = false;
    private bool exitDone = false;

    [SerializeField, Tooltip("Evento chiamato quando si entra nel trigger")]
    private UnityEvent onTriggerEnter;

    [SerializeField, Tooltip("Evento chiamato quando si resta nel trigger")]
    private UnityEvent onTriggerStay;

    [SerializeField, Tooltip("Evento chiamato quando si esce dal trigger")]
    private UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (once && enterDone) return;

            onTriggerEnter?.Invoke();
            enterDone = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (once && stayDone) return;

            onTriggerStay?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (once && exitDone) return;

            onTriggerExit?.Invoke();
            exitDone = true;
            stayDone = true;
        }
    }
}