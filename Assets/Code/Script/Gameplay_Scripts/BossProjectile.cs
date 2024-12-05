using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour, IDeflectable
{
    public bool Deflected;
    private Rigidbody _rigidBody;

    public Rigidbody Rigidbody => _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 1 << 16)
            return;

        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            damageable.Damage();

        this.gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        Deflected = false;
        this.gameObject.SetActive(false);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (Deflected)
    //    {
    //        Deflected = false;
    //    }
    //}
    public void Deflect(Vector3 dir, float strenght)
    {
        Deflected = true;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(dir * strenght, ForceMode.Impulse);
    }
}
