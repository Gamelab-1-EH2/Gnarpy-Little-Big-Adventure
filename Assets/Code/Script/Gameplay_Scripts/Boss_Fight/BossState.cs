using StateMachines.States;
using UnityEngine;

public abstract class BossState : State
{
    public BossState() : base()
    {

    }

    public abstract void TriggerEnter(Collider other);
    public abstract void TriggerExit(Collider other);
}
