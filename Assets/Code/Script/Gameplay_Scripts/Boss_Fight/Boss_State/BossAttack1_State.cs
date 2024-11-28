using UnityEngine;

public class BossAttack1_State : BossState
{
    BossController controller;
    public BossAttack1_State(BossController boss) : base()
    {
        this.controller = boss;
    }

    public override void Enter()
    {

    }

    public override void Process()
    {
        Debug.Log("Attack1");
        controller._animator.SetTrigger("Attack");


        controller.StartCoroutine(controller.Cooldown());
    }

    public override void Exit()
    {

    }

    public override void TriggerEnter(Collider other)
    {

    }

    public override string ToString() => "Attack1_State";
}
