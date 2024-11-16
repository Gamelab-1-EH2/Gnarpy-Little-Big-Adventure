using UnityEngine;

namespace Player.Model
{
    public class PlayerModel
    {
        private MovementModel _movementModel;
        private PlayerState _playerState;

        public PlayerModel(Player_SO playerSO, Rigidbody body)
        {
            _playerState = PlayerState.Idle;
            _movementModel = new MovementModel(playerSO, body);
        }

        public MovementModel Movement
        {
            get => _movementModel;
            set => _movementModel = value;
        }

        public PlayerState State
        {
            get => _playerState;
            set => _playerState = value;
        }
    }
}
