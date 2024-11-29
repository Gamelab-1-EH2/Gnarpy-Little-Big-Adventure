using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossController BossController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.GetComponent<MovableObject>().IsDeflected)
        {
            BossController.Damage();
        }
    }

}
