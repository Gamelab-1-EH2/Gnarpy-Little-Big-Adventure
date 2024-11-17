using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Model;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerClimb_State : PlayerState
    {
        private PlayerModel _playerModel;
        private Rigidbody _rigidBody;
        public PlayerClimb_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidBody = playerModel.Movement.Body;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Climb;

            _rigidBody.useGravity = false;
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);

            InputManager.ActionMap.Gameplay.Movement.performed += UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled += UpdateDirection;
        }

        public override void Process()
        {
            Vector3 velocity = _playerModel.Movement.Direction * _playerModel.Movement.Speed;
            _playerModel.Movement.Body.velocity = velocity;
        }

        public override void Exit()
        {
            _rigidBody.useGravity = true;

            InputManager.ActionMap.Gameplay.Movement.performed -= UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled -= UpdateDirection;
        }

        public override void TriggerEnter(Collider other)
        {
            
        }

        public override void TriggerExit(Collider other)
        {
            //Exit Climb
            if (other.gameObject.layer == 7)
                base.OnStateExit(new PlayerFall_State(_playerModel));
        }

        public override string ToString() => "Climb";
        
        private void UpdateDirection(InputAction.CallbackContext context)
        {
            Vector3 dir = context.ReadValue<Vector3>();
            _playerModel.Movement.Direction = dir;
        }

    }
}
