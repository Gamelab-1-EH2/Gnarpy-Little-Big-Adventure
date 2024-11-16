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

        protected override void ChangeState(StateMachines.States.State state)
        {
            base.ChangeState(state);
            //Debug.Log(state.ToString());
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
