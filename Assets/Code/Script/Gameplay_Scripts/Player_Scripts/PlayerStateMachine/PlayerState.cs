using UnityEngine;

using Player.Model;
using StateMachines.States;

namespace Player.Behaviour.States
{
    public abstract class PlayerState : State
    {
        protected PlayerModel _playerModel;
        private int _excludeLayer = (1 << 3) | (1 << 15);

        public PlayerState(PlayerModel playerModel) : base()
        {
            _playerModel = playerModel;
        }

        protected bool IsGrounded()
        {
            bool isGrounded = false;

            //Position for GroundCheck
            Vector3 groundCheckPos = _playerModel.Movement.RigidBody.transform.position;
            groundCheckPos.y += _playerModel.Rotation.y > 0f ? 0.8f : 0.2f;

            groundCheckPos.x += 0.48f;
            isGrounded = Physics.Raycast(groundCheckPos, _playerModel.Rotation, _playerModel.Movement.GroundCheckDistance, ~ _excludeLayer);
            Debug.DrawRay(groundCheckPos, _playerModel.Rotation * _playerModel.Movement.GroundCheckDistance, Color.magenta, Time.deltaTime);

            groundCheckPos.x += -0.98f;
            isGrounded |= Physics.Raycast(groundCheckPos, _playerModel.Rotation, _playerModel.Movement.GroundCheckDistance, ~ _excludeLayer);
            Debug.DrawRay(groundCheckPos, _playerModel.Rotation * _playerModel.Movement.GroundCheckDistance, Color.magenta, Time.deltaTime);
            
            return isGrounded;
        }

        public abstract void TriggerEnter(Collider other);
        public abstract void TriggerExit(Collider other);
    }
}
