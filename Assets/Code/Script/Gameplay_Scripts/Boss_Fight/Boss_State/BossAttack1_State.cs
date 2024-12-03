using Unity.VisualScripting;
using UnityEngine;

public class BossAttack1_State : BossState
{
    BossController controller;
    int i;
    BossView bossView;
    public BossAttack1_State(BossController boss, int i,BossView bossView) : base()
    {
        this.controller = boss;
        this.i= i;
        this.bossView = bossView;
    }

    public override void Enter()
    {

    }

    public override void Process()
    {
        Debug.Log("Attack1");
        bossView.Animator.SetTrigger("Attack");
        bossView.WarningSprite.transform.localScale = new Vector3(controller.PhaseSo[i].TentacleWidth, bossView.WarningSprite.transform.localScale.y, 1);
        bossView.WarningSprite.transform.position = controller.PlayerPos();
        controller.StartCoroutine(controller.DisplayWarning());
        controller.StartCoroutine(controller.Attack());
    }

    public override void Exit()
    {

    }

    public override void TriggerEnter(Collider other)
    {

    }

    public override string ToString() => "Attack1_State";
}
