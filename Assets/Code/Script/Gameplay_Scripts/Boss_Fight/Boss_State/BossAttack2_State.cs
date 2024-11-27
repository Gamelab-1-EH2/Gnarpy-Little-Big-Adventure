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
