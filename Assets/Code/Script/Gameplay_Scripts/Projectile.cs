using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{   
    [SerializeField] private bool IsPlayerProjectile = false;

    private Rigidbody _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 orientation, float velocity)
    {
        transform.eulerAngles = new Vector3(orientation.x * 90f, orientation.y * 90f, 0f);
        _rigidBody.velocity = Vector3.right * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            damageable.Damage();

        this.gameObject.SetActive(false);
    }
}