using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath_State : BossState
{

    BossController controller;
    int i;
    BossView bossView;
    public BossDeath_State(BossController boss, int i, BossView bossView) : base()
    {
        this.controller = boss;
        this.i = i;
        this.bossView = bossView;
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Process()
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }
}
