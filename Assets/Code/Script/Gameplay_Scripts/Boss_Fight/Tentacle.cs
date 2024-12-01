using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable component))
        {
            component.Damage();
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);    
        }
    }

}
