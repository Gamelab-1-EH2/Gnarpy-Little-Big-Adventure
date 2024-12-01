using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2_State : BossState
{
    BossController controller;
    int i;
    BossView bossView;
    public BossAttack2_State(BossController boss,int i,BossView bossView) : base()
    {
        this.controller = boss;
        this.i = i;
        this.bossView = bossView;
    }


    public override void Enter()
    {
       
    }

    public override void Process()
    {
        Debug.Log("Attack2");
        controller.BossView.Animator.SetTrigger("Attack");
        Vector3 direction= controller.PlayerPos() - controller.transform.position;
        controller.SpawnObject().SetActive(true);
        controller.SpawnObject().GetComponent<Rigidbody>().velocity = direction * Time.deltaTime * controller.PhaseSo[i].ProjectileSpeed;
        controller.StartCoroutine(controller.Attack());
    }

    public override void Exit()
    {

    }

    public override void TriggerEnter(Collider other)
    {

    }


    public override string ToString() => "Attack2_State";
}
