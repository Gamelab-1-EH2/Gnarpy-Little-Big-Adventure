using Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerIdle_State : PlayerState
    {
        private PlayerModel _playerModel;
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
        }

        private void HandleGroundCheck()
        {
            //Position for GroundCheck
            Vector3 globalPos = _rigidBody.transform.position;
            globalPos.y += _playerModel.Movement.GroundCheckOffset;

            //Check if is grounded
            if (!IsGrounded())
                base.OnStateExit?.Invoke(new PlayerFall_State(_playerModel));
        }

        private bool IsGrounded()
        {
            bool isGrounded = false;

            //Position for GroundCheck
            Vector3 globalPos = _rigidBody.transform.position;
            globalPos.y += _playerModel.Movement.GroundCheckOffset;

            globalPos.x += 0.5f;
            isGrounded = Physics.Raycast(globalPos, Vector3.down, _playerModel.Movement.GroundCheckDistance, ~(1 << 3));
            Debug.DrawRay(globalPos, Vector3.down * _playerModel.Movement.GroundCheckDistance, Color.red, Time.deltaTime);

            globalPos.x += -1f;
            isGrounded |= Physics.Raycast(globalPos, Vector3.down, _playerModel.Movement.GroundCheckDistance, ~(1 << 3));
            Debug.DrawRay(globalPos, Vector3.down * _playerModel.Movement.GroundCheckDistance, Color.red, Time.deltaTime);

            return isGrounded;
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
