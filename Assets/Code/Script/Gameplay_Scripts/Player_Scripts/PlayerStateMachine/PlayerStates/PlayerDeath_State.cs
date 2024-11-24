using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Model;

namespace Player.Behaviour.States
{
    public class PlayerDeath_State : PlayerState
    {
        private PlayerModel _playerModel;
        public PlayerDeath_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Dead;
            _playerModel.Movement.RigidBody.velocity = Vector3.zero;
            _playerModel.Movement.RigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }

        public override void Exit()
        {
            
        }

        public override void Process()
        {
            
        }

        public override void TriggerEnter(Collider other)
        {
            
        }

        public override void TriggerExit(Collider other)
        {
            
        }
        public override string ToString() => "Dead";
    }
}
