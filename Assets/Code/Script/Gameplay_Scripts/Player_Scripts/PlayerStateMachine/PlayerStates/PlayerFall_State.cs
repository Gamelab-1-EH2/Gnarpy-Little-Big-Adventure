using UnityEngine;

using Player.Model;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerFall_State : PlayerState
    {
        private PlayerModel _playerModel;
        private Rigidbody _rigidBody;
        public PlayerFall_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
            _rigidBody = playerModel.Movement.RigidBody;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Fall;
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);

            InputManager.ActionMap.Gameplay.Movement.performed += UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled += UpdateDirection;
        }

        public override void Exit()
        {
            InputManager.ActionMap.Gameplay.Movement.performed -= UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled -= UpdateDirection;
        }

        public override void Process()
        {
            HandleMovement();
            HandleGroundCheck();
        }

        private void HandleMovement()
        {
            //Apply fall movement control
            Vector3 directionForce = _playerModel.Movement.Direction * _playerModel.Movement.Fall.FallScalar;
            directionForce.y = 0;
            _rigidBody.velocity += directionForce;

            _rigidBody.AddForce(_playerModel.Movement.Fall.Gravity, ForceMode.Acceleration);
        }

        private void HandleGroundCheck()
        {
            //Position for GroundCheck
            Vector3 globalPos = _rigidBody.transform.position;
            globalPos.y += _playerModel.Movement.GroundCheckOffset;

            //Check if is grounded
            if(IsGrounded())
            {
                //Check Input
                Vector3 inputDir = InputManager.ActionMap.Gameplay.Movement.ReadValue<Vector3>();
                if (inputDir == Vector3.zero)    //To Idle
                    base.OnStateExit?.Invoke(new PlayerIdle_State(_playerModel));
                else                            //To Walk
                    base.OnStateExit?.Invoke(new PlayerWalk_State(_playerModel));
            }
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

        private void UpdateDirection(InputAction.CallbackContext context)
        {
            Vector3 dir = context.ReadValue<Vector3>();
            _playerModel.Movement.Direction = dir;
        }

        public override void TriggerEnter(Collider other)
        {
            //Climb
            if(other.gameObject.layer == 7)
                base.OnStateExit(new PlayerClimb_State(_playerModel));
        }

        public override void TriggerExit(Collider other)
        {

        }

        public override string ToString() => "Fall";
    }
}
