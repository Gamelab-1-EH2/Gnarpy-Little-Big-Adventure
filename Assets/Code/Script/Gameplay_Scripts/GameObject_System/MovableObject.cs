using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableObject : MonoBehaviour, IFallable, IDeflectable
{
    protected Rigidbody _rigidBody;

    private bool _canBreak = false;
    private bool _isFalling = false;
    
    protected virtual void Awake()
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

    public void UpdateFall() => _isFalling = _rigidBody.velocity.y < -0.1f;

    public bool IsDeflected => _canBreak;

    public Rigidbody Rigidbody => _rigidBody;

    private void OnCollisionEnter(Collision collision)
    {
        if(_canBreak && _isFalling)
            this.gameObject.SetActive(false);
    }
}
