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

            if (Physics.OverlapSphere(_rigidBody.transform.position, 0.5f, 1<<7).Length > 0)
                base.OnStateExit?.Invoke(new PlayerClimb_State(_playerModel));

            InputManager.ActionMap.Gameplay.Movement.performed += UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled += UpdateDirection;
        }

        public override void Exit()
        {
            InputManager.ActionMap.Gameplay.Movement.performed -= UpdateDirection;
            InputManager.ActionMap.Gameplay.Movement.canceled -= UpdateDirection;
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
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
