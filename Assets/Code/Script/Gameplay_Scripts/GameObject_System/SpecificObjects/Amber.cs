using UnityEngine;

public class Amber : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IndestructibleObject>(out IndestructibleObject _))
        {
            this.gameObject.SetActive(false);
        }
    }
}
