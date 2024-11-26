using StateMachines;
using StateMachines.States;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    public BossStateMachine(BossState initialState) : base(initialState)
    {

    }

    protected override void ChangeState(State state)
    {
        base.ChangeState(state);
        Debug.Log(state.ToString());
    }

    public void OnTriggerEnter(Collider collider)
    {
        BossState bossState = (BossState)base._currentState;
        bossState.TriggerEnter(collider);
    }

}
