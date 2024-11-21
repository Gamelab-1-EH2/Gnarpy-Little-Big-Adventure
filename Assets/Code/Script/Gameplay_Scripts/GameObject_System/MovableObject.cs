using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableObject : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private bool _canBreak = false;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public virtual void ApplyGravity(Vector3 dir, float strenght)
    {
        dir.z = 0;
        _canBreak = true;
        _rigidBody.AddForce(dir * strenght, ForceMode.Force);
    }

    public virtual void Deflect(Vector3 dir, float strenght)
    {
        if (!_canBreak)
        {
            _rigidBody.velocity = Vector3.zero;
            _canBreak = true;
        }
        
        dir.z = 0;
        _rigidBody.AddForce(dir * strenght, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_canBreak)
            this.gameObject.SetActive(false);
    }
}
