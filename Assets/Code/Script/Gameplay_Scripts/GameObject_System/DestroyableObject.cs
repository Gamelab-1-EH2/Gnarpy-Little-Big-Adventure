using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DestroyableObject : MovableObject, IDestroyable, IDamageable
{
    public Action<DestroyableObject> OnDestroy;

    [SerializeField] private Transform _normalObject;
    [SerializeField] private Transform _destroyedObject;

    [SerializeField] private bool _canDrop = false;
    public bool CanDrop => _canDrop;

    protected override void Awake()
    {
        base.Awake();

        _normalObject?.gameObject.SetActive(true);
        _destroyedObject?.gameObject.SetActive(false);
    }

    public virtual void Damage() => Destroy();
    public virtual void Destroy()
    {
        _rigidBody.isKinematic = true;
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;

        OnDestroy?.Invoke(this);
        OnDestroy -= OnDestroy;

        _normalObject?.gameObject.SetActive(false);
        _destroyedObject?.gameObject.SetActive(true);
    }
}
