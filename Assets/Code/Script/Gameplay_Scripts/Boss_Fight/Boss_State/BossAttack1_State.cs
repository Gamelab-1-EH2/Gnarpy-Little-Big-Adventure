using Unity.VisualScripting;
using UnityEngine;

public class BossAttack1_State : BossState
{
    BossController controller;
    int i;
    public BossAttack1_State(BossController boss, int i) : base()
    {
        this.controller = boss;
        this.i= i;
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
