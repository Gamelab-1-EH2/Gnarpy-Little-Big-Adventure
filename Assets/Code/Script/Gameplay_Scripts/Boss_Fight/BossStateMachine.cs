using StateMachines;
using StateMachines.States;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    private static BossState initialState;

    public BossStateMachine() : base(initialState)
    {

    }

    protected override void ChangeState(State state)
    {
        base.ChangeState(state);
        Debug.Log(state.ToString());
    }

    public void OnTriggerEnter(Collider collider)
    {
        BossState playerState = (BossState)base._currentState;
        playerState.TriggerEnter(collider);
    }

    public void OnTriggerExit(Collider collider)
    {
        BossState bossState = (BossState)base._currentState;
        bossState.TriggerExit(collider);
    }
}
