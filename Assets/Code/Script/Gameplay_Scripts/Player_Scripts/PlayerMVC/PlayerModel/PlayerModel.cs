using System;
using UnityEngine;

namespace Player.Model
{
    public class PlayerModel
    {
        public Action<int> OnHPChanged;
        public Action<int> OnBallOfWoolCollected;

        private MovementModel _movementModel;
        private PowerUpModel _powerUpModel;
        private PlayerState _playerState;

        private int _healtPoints;
        private int _ballsOfWool;

        public PlayerModel(Player_SO playerSO, Rigidbody body, Transform shieldTransform)
        {
            _playerState = PlayerState.Idle;
            _healtPoints = playerSO.HealthPoints;
            _movementModel = new MovementModel(playerSO, body);
            _powerUpModel = new PowerUpModel(playerSO, shieldTransform);
        }

        public MovementModel Movement
        {
            get => _movementModel;
            set => _movementModel = value;
        }

        public PowerUpModel PowerUp
        {
            get => _powerUpModel;
            set => _powerUpModel = value;
        }

        public PlayerState State
        {
            get => _playerState;
            set => _playerState = value;
        }

        public int HealthPoints
        {
            get => _healtPoints;
            set
            {
                _healtPoints = value;
                OnHPChanged?.Invoke(_healtPoints);
            }
        }

        public int BallOfWool
        {
            get => _ballsOfWool;
            set
            {
                _ballsOfWool = value;
                OnBallOfWoolCollected?.Invoke(_ballsOfWool);
            }
        }

        public void Disconnect()
        {
            OnHPChanged -= OnHPChanged;
            PowerUp.Disconnect();
        }

    }
}
