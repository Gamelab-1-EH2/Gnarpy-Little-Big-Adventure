using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IDeflectable
{
    protected Rigidbody _rigidBody;
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
        if (collision.gameObject.layer == 1<<16)
            return;

        if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            damageable.Damage();

        this.gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    public void Deflect(Vector3 dir, float strenght)
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(dir * strenght, ForceMode.Impulse);
    }
}