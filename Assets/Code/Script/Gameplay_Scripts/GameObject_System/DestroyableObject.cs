using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DestroyableObject : MonoBehaviour, IDestroyable, IFallable, IDamageable
{
    public Action<DestroyableObject> OnDestroy;

    [SerializeField] private bool _canDrop = false;
    public bool CanDrop => _canDrop;

    protected bool _isFalling;
    protected Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _isFalling = false;
    }

    public virtual void Damage() => Destroy();
    public virtual void Destroy()
    {
        OnDestroy?.Invoke(this);
        OnDestroy -= OnDestroy;
        this.gameObject.SetActive(false);
    }
    
    public void UpdateFall()
    {
        _isFalling = _rigidBody.velocity.y > 0.1f;
    }

    public void StartObject()
    {
        
    }
}
