using Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerWalk_State : PlayerState
    {
        private PlayerModel _playerModel;
        private Rigidbody _rigidBody;

        public PlayerWalk_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidBody = _playerModel.Movement.RigidBody;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Move;
            
            _playerModel.Movement.Direction = InputManager.ActionMap.Gameplay.Movement.ReadValue<Vector3>();

            InputManager.ActionMap.Gameplay.Movement.performed += UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled += UpdateDirection;
            InputManager.ActionMap.Gameplay.Jump.started += ExitToJump;
        }

        public override void Process()
        {
            if (_playerModel.Movement.CanMove)
                HandleMovement();

            HandleGroundCheck();
        }

        private void HandleMovement()
        {
            //Handle Movement
            Vector3 velocity = _playerModel.Movement.Direction * _playerModel.Movement.Speed;
            velocity.y = _playerModel.Movement.RigidBody.velocity.y;
            _playerModel.Movement.RigidBody.velocity = velocity;
        }

        private void HandleGroundCheck()
        {
            //Position for GroundCheck
            Vector3 globalPos = _rigidBody.transform.position;
            globalPos.y += _playerModel.Movement.GroundCheckOffset;

            //Check if is grounded
            if(!IsGrounded())
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

        public override void Exit()
        {
            InputManager.ActionMap.Gameplay.Movement.performed -= UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled -= UpdateDirection;
            InputManager.ActionMap.Gameplay.Jump.started -= ExitToJump;
        }

        private void UpdateDirection(InputAction.CallbackContext context)
        {
            Vector3 dir = context.ReadValue<Vector3>();
            _playerModel.Movement.Direction = dir;

            if(dir == Vector3.zero)
                base.OnStateExit?.Invoke(new PlayerIdle_State(_playerModel));
        }

        private void ExitToJump(InputAction.CallbackContext _) => base.OnStateExit?.Invoke(new PlayerJump_State(_playerModel));

        public override void TriggerEnter(Collider other)
        {
            //Climb
            if (other.gameObject.layer == 7)
                base.OnStateExit(new PlayerClimb_State(_playerModel));
        }

        public override void TriggerExit(Collider other)
        {

        }

        public override string ToString() => "Walk";

    }
}
