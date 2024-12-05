using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [System.Serializable]
    public class TeleportTarget
    {
        public KeyCode key;
        public Transform targetTransform;
    }

    public TeleportTarget[] teleportTargets;

    void FixedUpdate()
    {
        foreach (var teleportTarget in teleportTargets)
        {
            if (Input.GetKeyDown(teleportTarget.key))
            {
                TeleportToTarget(teleportTarget.targetTransform);
                break;
            }
        }
    }

    void TeleportToTarget(Transform targetTransform)
    {
        if (targetTransform != null)
        {
            transform.position = targetTransform.position;
        }
    }
}