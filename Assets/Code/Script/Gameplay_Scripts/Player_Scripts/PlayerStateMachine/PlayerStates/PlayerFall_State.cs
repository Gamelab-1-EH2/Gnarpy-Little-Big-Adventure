using UnityEngine;

using Player.Model;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerFall_State : PlayerState
    {
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

            _rigidBody.AddForce(_playerModel.Movement.Fall.Gravity * -_playerModel.Rotation.y, ForceMode.Acceleration);

            if (_rigidBody.transform.position.y > _playerModel.MaxY || _rigidBody.transform.position.y < _playerModel.MinY)
            {
                _rigidBody.transform.position = _playerModel.Movement.LastAllowedPosition;
                _rigidBody.velocity = Vector3.zero;

                _playerModel.HealthPoints -= 1;
            }
        }

        private void HandleGroundCheck()
        {
            //Check if is grounded
            if(base.IsGrounded())
            {
                //Check Input
                 Vector3 inputDir = InputManager.ActionMap.Gameplay.Movement.ReadValue<Vector3>();
                if (inputDir == Vector3.zero)    //To Idle
                    base.OnStateExit?.Invoke(new PlayerIdle_State(_playerModel));
                else                            //To Walk
                    base.OnStateExit?.Invoke(new PlayerWalk_State(_playerModel));
            }
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
