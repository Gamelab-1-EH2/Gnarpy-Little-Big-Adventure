using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shield : MonoBehaviour
{
    private Rigidbody _body;
    private float _deflectionForce;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _body.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IDeflectable>(out IDeflectable deflectable))
        {
            Vector3 dir = deflectable.Rigidbody.velocity;
            dir.z = 0f;
            deflectable.Deflect(-dir, _deflectionForce);
        }
    }

    public float DeflectionForce
    {
        get => _deflectionForce;
        set
        {
            if (value < 0)
                value = 0;

            _deflectionForce = value;
        }
    }
}
