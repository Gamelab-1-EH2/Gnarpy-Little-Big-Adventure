using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamArenaTrigger : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float triggerX = 10f;
    [SerializeField] private string triggerName = "CamEnterBoss";
    private bool animationTriggered = false;

    void Start()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInParent<Animator>();
            if (playerAnimator == null)
            {
                Debug.LogError("No Animator");
            }
        }
    }

    void Update()
    {
        if (!animationTriggered && transform.position.x > triggerX)
        {
            TriggerAnimation();
        }
    }

    private void TriggerAnimation()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger(triggerName);
        }
        transform.SetParent(null);
        animationTriggered = true;
    }
}
