using UnityEngine;

using Player.Model;
using StateMachines.States;

namespace Player.Behaviour.States
{
    public abstract class PlayerState : State
    {
        public PlayerState(PlayerModel playerModel) : base()
        {

        }

        public abstract void TriggerEnter(Collider other);
        public abstract void TriggerExit(Collider other);
    }
}
