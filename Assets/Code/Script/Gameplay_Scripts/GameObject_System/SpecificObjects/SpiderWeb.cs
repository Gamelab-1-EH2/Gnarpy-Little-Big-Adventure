using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpiderWeb : MonoBehaviour
{
    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IndestructibleObject>(out IndestructibleObject _))
            this.gameObject.SetActive(false);

        if (collision.gameObject.TryGetComponent<DestroyableObject>(out DestroyableObject _))
            this.gameObject.SetActive(false);
    }
}
