using UnityEngine;

using Player;

public class SpiderWeb : MonoBehaviour
{
    [SerializeField] private float BouceForce = 25.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IndestructibleObject>(out IndestructibleObject _))
        {
            this.gameObject.SetActive(false);
            return;
        }

        if(collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            rb.AddForce(rb.transform.up * BouceForce, ForceMode.Impulse);
        }
    }
}
