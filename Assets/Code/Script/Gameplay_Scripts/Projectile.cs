using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 direction, float velocity)
    {
        transform.eulerAngles = direction;
        _rigidBody.velocity = transform.forward * velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            damageable.Damage();

        this.gameObject.SetActive(false);
    }
}