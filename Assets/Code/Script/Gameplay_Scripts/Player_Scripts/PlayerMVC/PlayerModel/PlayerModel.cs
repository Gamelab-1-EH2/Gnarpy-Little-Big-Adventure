using System;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace Player.Model
{
    public class PlayerModel
    {
        public Action<int> OnHPChanged;
        public Action<int> OnBallOfWoolCollected;
        public Action<PlayerState> OnStateChanged;

        private MovementModel _movementModel;
        private PowerUpModel _powerUpModel;
        private PlayerState _playerState;

        private int _healtPoints;
        private int _ballsOfWool;

        private Vector3 _rotation;

        private float _maxY;
        private float _minY;

        public PlayerModel(Player_SO playerSO, Rigidbody body, Transform shieldTransform)
        {
            _playerState = PlayerState.Idle;
            _healtPoints = playerSO.HealthPoints;

            _maxY = playerSO.MaxY;
            _minY = playerSO.MinY;

            _movementModel = new MovementModel(playerSO, body);
            _powerUpModel = new PowerUpModel(playerSO, shieldTransform);
            _rotation = Vector3.down;
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
            set
            {
                _playerState = value;
                OnStateChanged?.Invoke(value);
            }
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

        public float MaxY => _maxY;
        public float MinY => _minY;

        public int BallOfWool
        {
            get => _ballsOfWool;
            set
            {
                _ballsOfWool = value;
                OnBallOfWoolCollected?.Invoke(_ballsOfWool);
            }
        }

        public Vector3 Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }

        public void Disconnect()
        {
            OnHPChanged -= OnHPChanged;
            PowerUp.Disconnect();
        }

    }
}
