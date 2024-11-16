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
            _rigidBody = playerModel.Movement.Body;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Fall;

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
            HandleGroundCheck();

            //Apply fall movement control
            Vector3 directionForce = _playerModel.Movement.Direction * _playerModel.Movement.FallScalar;
            _rigidBody.velocity += directionForce;
        }

        private void HandleGroundCheck()
        {
            //Position for GroundCheck
            Vector3 globalPos = _rigidBody.transform.position;
            globalPos.y += _playerModel.Movement.GroundCheckOffset;

            //Debug
            Debug.DrawRay(globalPos, Vector3.down * _playerModel.Movement.GroundCheckDistance, Color.red, Time.deltaTime);

            //Check if is grounded
            if (Physics.Raycast(globalPos, Vector3.down, _playerModel.Movement.GroundCheckDistance, 1 << 6))
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

        }

        public override void TriggerExit(Collider other)
        {

        }

        public override string ToString() => "Fall";
    }
}
