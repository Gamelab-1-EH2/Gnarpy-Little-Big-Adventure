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

        public PlayerJump_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidbody = _playerModel.Movement.Body;
            
            jumpStartTime = 0;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Jump;
            jumpStartTime = Time.time;

            InputManager.ActionMap.Gameplay.Jump.canceled += EndJump;
            InputManager.ActionMap.Gameplay.Movement.performed += UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled += UpdateDirection;
        }

        public override void Process()
        {
            float enlapsedTime = Time.time - jumpStartTime;
            float jumpProgress =  enlapsedTime / _playerModel.Movement.JumpTime;

            Vector3 vel = _rigidbody.velocity;
            vel.x = _rigidbody.velocity.x + (_playerModel.Movement.Direction.x * _playerModel.Movement.FallScalar);
            vel.y = _playerModel.Movement.JumpCurve.Evaluate(jumpProgress) * _playerModel.Movement.JumpForce;

            _rigidbody.velocity = vel;

            //Exit state condition
            if (jumpProgress >= 1f)
                base.OnStateExit?.Invoke(new PlayerFall_State(_playerModel));
        }

        public override void Exit()
        {
            InputManager.ActionMap.Gameplay.Jump.canceled -= EndJump;
            InputManager.ActionMap.Gameplay.Movement.performed -= UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled -= UpdateDirection;
        }

        public override void TriggerEnter(Collider other)
        {
            //Climb
            if (other.gameObject.layer == 7)
                base.OnStateExit(new PlayerClimb_State(_playerModel));
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
