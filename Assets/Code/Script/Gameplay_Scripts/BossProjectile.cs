using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : Projectile
{
    private bool _deflected;
    public bool Deflected=>_deflected;
    //private void Awake()
    //{
    //    _rigidBody = GetComponent<Rigidbody>();
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == 1 << 16)
    //        return;

    //    if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
    //        damageable.Damage();

    //    this.gameObject.SetActive(false);
    //}

    //private void OnBecameInvisible()
    //{
    //    this.gameObject.SetActive(false);
    //}
    public void Deflect(Vector3 dir, float strenght)
    {
        _deflected = true;
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(dir * strenght, ForceMode.Impulse);
    }
}
