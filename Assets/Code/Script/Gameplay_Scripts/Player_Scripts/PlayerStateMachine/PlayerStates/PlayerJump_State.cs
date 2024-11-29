using Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerJump_State : PlayerState
    {
        private float jumpStartTime;
        
        private Rigidbody _rigidBody;
        private bool _isForced;

        public PlayerJump_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidBody = _playerModel.Movement.RigidBody;
            _isForced = false;
            jumpStartTime = 0;
        }

        public PlayerJump_State(PlayerModel playerModel, bool forced) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidBody = _playerModel.Movement.RigidBody;
            _isForced = forced;
            jumpStartTime = 0;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Jump;
            jumpStartTime = Time.time;

            if(!_isForced)
                InputManager.ActionMap.Gameplay.Jump.canceled += EndJump;

            InputManager.ActionMap.Gameplay.Movement.performed += UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled += UpdateDirection;
        }

        public override void Process()
        {
            float enlapsedTime = Time.time - jumpStartTime;
            float jumpProgress =  enlapsedTime / _playerModel.Movement.Jump.JumpTime;

            Vector3 vel = _rigidBody.velocity;
            vel.x = _rigidBody.velocity.x + (_playerModel.Movement.Direction.x * _playerModel.Movement.Fall.FallScalar);
            vel.y = _playerModel.Movement.Jump.JumpCurve.Evaluate(jumpProgress) * _playerModel.Movement.Jump.JumpForce * _playerModel.Movement.RigidBody.transform.up.y;
            vel.y *= -_playerModel.Rotation.y;

            if (_isForced)
                vel.y *= 2f;

            _rigidBody.velocity = vel;

            //Exit state condition
            if (jumpProgress >= 1f)
            {
                if (Physics.OverlapSphere(_rigidBody.transform.position, 0.5f, 1 << 7).Length > 0)
                    base.OnStateExit?.Invoke(new PlayerClimb_State(_playerModel));
                else
                    base.OnStateExit?.Invoke(new PlayerFall_State(_playerModel));
            }
        }

        public override void Exit()
        {
            if (!_isForced)
                InputManager.ActionMap.Gameplay.Jump.canceled -= EndJump;

            InputManager.ActionMap.Gameplay.Movement.performed -= UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled -= UpdateDirection;
        }

        public override void TriggerEnter(Collider other)
        {

        }

        public override void TriggerExit(Collider other)
        {

        }

        private void UpdateDirection(InputAction.CallbackContext context)
        {
            Vector3 dir = context.ReadValue<Vector3>();
            _playerModel.Movement.Direction = dir;
        }

        private void EndJump(InputAction.CallbackContext _) => base.OnStateExit?.Invoke(new PlayerFall_State(_playerModel));

        public override string ToString() => "Jump";

    }
}
