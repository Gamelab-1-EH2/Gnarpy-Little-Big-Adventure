using UnityEngine;

using Player.Model;

namespace Player.Behaviour.States
{
    public class PlayerFall_State : PlayerState
    {
        private PlayerModel _playerModel;
        public PlayerFall_State(PlayerModel playerModel) : base(playerModel)
        {
            _playerModel = playerModel;
        }

        public override void Enter()
        {
            _playerModel.State = Model.PlayerState.Fall;
        }

        public override void Exit()
        {
            
        }

        public override void Process()
        {
            Vector3 globalPos = _playerModel.Movement.Body.transform.position;
            globalPos.y += _playerModel.Movement.GroundCheckOffset;

            Debug.DrawRay(globalPos, Vector3.down * _playerModel.Movement.GroundCheckDistance, Color.red, Time.deltaTime);

            //Check if is grounded
            if(Physics.Raycast(globalPos, Vector3.down, _playerModel.Movement.GroundCheckDistance, 1<<6))
            {
                //Check Input
                Vector3 inputDir = InputManager.ActionMap.Gameplay.Movement.ReadValue<Vector3>();
                if(inputDir == Vector3.zero)    //To Idle
                    base.OnStateExit?.Invoke(new PlayerIdle_State(_playerModel));
                else                            //To Walk
                    base.OnStateExit?.Invoke(new PlayerWalk_State(_playerModel));
            }
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
