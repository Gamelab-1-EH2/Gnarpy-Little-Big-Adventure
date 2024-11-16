using Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Behaviour.States
{
    public class PlayerWalk_State : PlayerState
    {
        private PlayerModel _playerModel;
        public PlayerWalk_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
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
            if (!_playerModel.Movement.CanMove)
                return;

            //Handle Movement
            Vector3 velocity = _playerModel.Movement.Direction * _playerModel.Movement.Speed;
            velocity.y = _playerModel.Movement.Body.velocity.y;
            _playerModel.Movement.Body.velocity = velocity;
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
            
        }

        public override void TriggerExit(Collider other)
        {

        }

        public override string ToString() => "Walk";

    }
}
