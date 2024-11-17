using UnityEngine;

using StateMachines;
using Player.Behaviour.States;

namespace Player.Behaviour.Machine
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStateMachine(PlayerState initialState) : base(initialState)
        {

        }

        public void PushState(PlayerState state) => base.ChangeState(state);
        
        public void OnTriggerEnter(Collider collider)
        {
            PlayerState playerState = (PlayerState)base._currentState;
            playerState.TriggerEnter(collider);
        }

        public void OnTriggerExit(Collider collider)
        {
            PlayerState playerState = (PlayerState)base._currentState;
            playerState.TriggerExit(collider);
        }
    }
}
