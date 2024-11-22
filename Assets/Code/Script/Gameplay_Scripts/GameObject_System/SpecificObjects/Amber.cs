using UnityEngine;

public class Amber : IndestructibleObject
{
    protected override void Awake()
    {
        base.Awake();
        base._rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IndestructibleObject>(out IndestructibleObject _))
        {
            this.gameObject.SetActive(false);
        }
    }
}
