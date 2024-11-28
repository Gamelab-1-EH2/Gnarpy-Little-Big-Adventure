using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2_State : BossState
{
    BossController controller;
    public BossAttack2_State(BossController boss) : base()
    {
        this.controller = boss;
    }


    public override void Enter()
    {
       
    }

    public override void Process()
    {
        Debug.Log("Attack2");
        controller._animator.SetTrigger("Attack");
        controller.objectThrown= controller.SpawnObject();
        Vector3 direction= controller.playerTransform- controller.Test.position;
        controller.objectThrown.GetComponent<Rigidbody>().velocity = direction*Time.deltaTime*controller.phase_So[controller.i].ProjectileSpeed;
        controller.StartCoroutine(controller.Cooldown());
    }

    public override void Exit()
    {

    }

    public override void TriggerEnter(Collider other)
    {

    }


    public override string ToString() => "Attack2_State";
}
