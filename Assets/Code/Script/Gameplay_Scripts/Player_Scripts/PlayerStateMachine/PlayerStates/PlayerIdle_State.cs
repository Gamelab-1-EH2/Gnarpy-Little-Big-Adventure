using Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerIdle_State : PlayerState
    {
        private Rigidbody _rigidBody;
        public PlayerIdle_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidBody = playerModel.Movement.RigidBody;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Idle;

            //Update Player Movement
            _playerModel.Movement.Direction = Vector3.zero;
            _playerModel.Movement.RigidBody.velocity = _playerModel.Movement.Direction;

            //Connect Actions
            InputManager.ActionMap.Gameplay.Movement.performed += ExitToWalk;
            InputManager.ActionMap.Gameplay.Jump.started += ExitToJump;
        }

        public override void Exit()
        {
            //Disconnect Actions
            InputManager.ActionMap.Gameplay.Movement.performed -= ExitToWalk;
            InputManager.ActionMap.Gameplay.Jump.started -= ExitToJump;
        }

        public override void Process()
        {
            HandleGroundCheck();
            HandleMovement();
        }

        private void HandleGroundCheck()
        {
            //Check if is grounded
            if (!base.IsGrounded())
                base.OnStateExit?.Invoke(new PlayerFall_State(_playerModel));
        }

        private void HandleMovement()
        {
            //Apply fall movement control
            _rigidBody.AddForce(_playerModel.Movement.Fall.Gravity * -_playerModel.Rotation.y, ForceMode.Acceleration);
        }

        private void ExitToWalk(InputAction.CallbackContext context)
        {
            Vector3 dir = context.ReadValue<Vector3>();
            if (dir.x == 0)
                return;

            _playerModel.Movement.Direction = dir;
            base.OnStateExit?.Invoke(new PlayerWalk_State(_playerModel));
        }

        private void ExitToJump(InputAction.CallbackContext _) => base.OnStateExit?.Invoke(new PlayerJump_State(_playerModel));

        public override void TriggerEnter(Collider other)
        {

        }

        public override void TriggerExit(Collider other)
        {

        }

        public override string ToString() => "Idle";
    }
}
