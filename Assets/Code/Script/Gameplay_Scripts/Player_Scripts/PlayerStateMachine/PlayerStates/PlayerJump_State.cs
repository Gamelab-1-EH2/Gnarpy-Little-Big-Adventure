using Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerJump_State : PlayerState
    {
        private float jumpStartTime;

        private PlayerModel _playerModel;
        private Rigidbody _rigidbody;

        private bool _isJumping;

        public PlayerJump_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidbody = _playerModel.Movement.Body;
            jumpStartTime = 0;
            _isJumping = false;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Jump;
            jumpStartTime = Time.time;

            InputManager.ActionMap.Gameplay.Jump.canceled += Jump;
            InputManager.ActionMap.Gameplay.Movement.performed += UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled += UpdateDirection;
        }

        public override void Process()
        {
            if(!_isJumping)
            {
                //Check Hold Timer
                if (Time.time - jumpStartTime >= _playerModel.Movement.LongJumpTime)
                    Jump();
            }
            else
            {
                if (_rigidbody.velocity.y < 0)
                {
                    base.OnStateExit(new PlayerFall_State(_playerModel));
                    return;
                }

                Vector3 directionForce = _playerModel.Movement.Direction * _playerModel.Movement.FallScalar;
                _rigidbody.velocity += directionForce;
            }
        }

        public override void Exit()
        {
            InputManager.ActionMap.Gameplay.Jump.canceled -= Jump;
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

        private void Jump(InputAction.CallbackContext _) => Jump();
        private void Jump()
        {
            if (_isJumping)
                return;
            
            float holdTime = Time.time - jumpStartTime;
            float jumpForceScalar = Mathf.Clamp01(holdTime / _playerModel.Movement.LongJumpTime);
            float jumpForce = _playerModel.Movement.GetJumpForce(jumpForceScalar);

            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isJumping = true;
        }

        public override string ToString() => "Jump";

    }
}
