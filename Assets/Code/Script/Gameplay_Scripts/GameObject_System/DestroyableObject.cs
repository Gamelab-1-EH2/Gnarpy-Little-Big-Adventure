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

        if(_normalObject != null)
            _normalObject.gameObject.SetActive(true);

        if (_destroyedObject != null)
            _destroyedObject?.gameObject.SetActive(false);
    }

    public virtual void Damage() => Destroy();
    public virtual void Destroy()
    {
        _rigidBody.isKinematic = true;
        _rigidBody.constraints = RigidbodyConstraints.FreezeAll;

        OnDestroy?.Invoke(this);
        OnDestroy -= OnDestroy;


        if (_normalObject != null)
            _normalObject.gameObject.SetActive(false);

        if (_destroyedObject != null)
            _destroyedObject.gameObject.SetActive(true);
    }
}
