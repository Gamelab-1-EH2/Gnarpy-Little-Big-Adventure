using Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerJump_State : PlayerState
    {
        private float jumpStartTime;

        private PlayerModel _playerModel;
        private Rigidbody rigidbody;

        private bool isPreJump;

        public PlayerJump_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            rigidbody = _playerModel.Movement.Body;
            jumpStartTime = 0;

            InputManager.ActionMap.Gameplay.Jump.canceled += Jump;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Jump;
            jumpStartTime = Time.time;
            isPreJump = true;
        }

        public override void Process()
        {
            if (isPreJump)
                if (Time.time - jumpStartTime >= _playerModel.Movement.LongJumpTime)
                    Jump();


            if (rigidbody.velocity.y < 0)
            {
                base.OnStateExit(new PlayerFall_State(_playerModel));
                return;
            }
        }

        public override void Exit()
        {
            InputManager.ActionMap.Gameplay.Jump.canceled -= Jump;
        }

        public override void TriggerEnter(Collider other)
        {
            
        }

        public override void TriggerExit(Collider other)
        {

        }

        private void Jump(InputAction.CallbackContext _) => Jump();
        private void Jump()
        {
            if (!isPreJump)
                return;

            float holdTime = Time.time - jumpStartTime;
            float jumpForceScalar = holdTime / _playerModel.Movement.LongJumpTime;
            rigidbody.AddForce(Vector3.up * _playerModel.Movement.GetJumpForce(jumpForceScalar), ForceMode.Impulse);
            isPreJump = false;
        }

        public override string ToString() => "Jump";

    }
}
