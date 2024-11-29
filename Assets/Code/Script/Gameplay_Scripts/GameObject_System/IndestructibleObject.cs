using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IndestructibleObject : MonoBehaviour, IFallable
{
    protected bool _isFalling;
    protected Rigidbody _rigidBody;

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _isFalling = false;
    }

    public void UpdateFall()
    {
        _isFalling = _rigidBody.velocity.y > 0.1f;
    }

    public void StartObject()
    {
        
    }
}
