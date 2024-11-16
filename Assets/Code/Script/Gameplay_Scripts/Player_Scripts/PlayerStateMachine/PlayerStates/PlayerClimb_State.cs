using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Model;

namespace Player.Behaviour.States
{
    public class PlayerClimb_State : PlayerState
    {
        private PlayerModel _playerModel;

        public PlayerClimb_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
        }

        public override void Enter()
        {
            
        }

        public override void Process()
        {

        }

        public override void Exit()
        {
            
        }

        public override void TriggerEnter(Collider other)
        {
            
        }

        public override void TriggerExit(Collider other)
        {

        }

        public override string ToString() => "Climb";

    }
}
