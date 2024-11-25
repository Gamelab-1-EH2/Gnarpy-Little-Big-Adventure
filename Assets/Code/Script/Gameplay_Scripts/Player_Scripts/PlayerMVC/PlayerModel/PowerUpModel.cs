using System;
using UnityEngine;

using Collectible_System.PowerUp;

namespace Player.Model
{
    public class PowerUpModel
    {
        public Action<PowerUpType> OnPowerUpUnlock;

        public Action<float> OnRedProgressChanged;        
        private float _redPowerUpRadious;
        private float _redPowerUpStrenght;
        private float _redDelay;
        private float _redDuration;
        private float _redDelayProgress;

        public Action<float> OnGreenProgressChanged;
        private float _greenPowerUpRadious;
        private float _greenPowerUpStrenght;
        private Vector3 _greenPowerUpOffset;
        private Transform _shieldTransform;
        private float _greenDelay;
        private float _greenDuration;
        private float _greenDelayProgress;

        public Action<float> OnBlueProgressChanged;
        private GameObject _bluePowerUpProjectile;
        private float _projectileSpeed;
        private Vector3 _projectileOffset;
        private Vector2 _projectileDirection;
        private float _blueShootDelay;
        private float _blueDelayProgress;

        public PowerUpModel(Player_SO playerSO, Transform shieldTransform)
        {
            _redPowerUpRadious = playerSO.RedPowerUpRadius;
            _redPowerUpStrenght = playerSO.RedPowerUpStrenght;
            _redDelay = playerSO.RedPowerUpDelay;
            _redDuration = playerSO.RedPowerUpDuration;

            _greenPowerUpRadious = playerSO.GreenPowerUpRadius;
            _greenPowerUpStrenght = playerSO.GreenPowerUpStrenght;
            _greenPowerUpOffset = playerSO.GreenPowerUpOffset;
            _greenDelay = playerSO.GreenPowerUpDelay;
            _greenDuration = playerSO.GreenPowerUpDuration;
            _shieldTransform = shieldTransform;

            _bluePowerUpProjectile = playerSO.BluePowerUpProjectile;
            _projectileSpeed = playerSO.ProjectileSpeed;
            _projectileDirection = playerSO.ShootDirection;
            _projectileOffset = playerSO.ShootOffset;
            _blueShootDelay = playerSO.ShootDelay;
        }

        public float RedPowerUpRadious => _redPowerUpRadious;
        public float RedPowerUpStrenght => _redPowerUpStrenght;
        public float RedDelay => _redDelay;
        public float RedDuration => _redDuration;
        public float RedProgress
        {
            get => _redDelayProgress;
            set
            {
                _redDelayProgress = value;
                OnRedProgressChanged?.Invoke(value);
            }
        }

        public float GreenPowerUpRadious => _greenPowerUpRadious;
        public float GreenPowerUpStrenght => _greenPowerUpStrenght;
        public Vector3 GreenPowerUpOffset => _greenPowerUpOffset;
        public Transform ShieldTransform => _shieldTransform;
        public float GreenDelay => _greenDelay;
        public float GreenDuration => _greenDuration;
        public float GreenProgress
        {
            get => _greenDelayProgress;
            set
            {
                _greenDelayProgress = value;
                OnGreenProgressChanged?.Invoke(value);
            }
        }

        public GameObject BlueProjectile => _bluePowerUpProjectile;
        public float ProjectileSpeed => _projectileSpeed;
        public Vector3 ProjectileDirection => _projectileDirection;
        public Vector2 ProjectileOffset => _projectileOffset;
        public float ShootDelay => _blueShootDelay;
        public float BlueProgress
        {
            get => _blueDelayProgress;
            set
            {
                _blueDelayProgress = value;
                OnBlueProgressChanged?.Invoke(value);
            }
        }

        public void UnlockPowerUp(PowerUpType powerUpType) => OnPowerUpUnlock?.Invoke(powerUpType);

        public void Disconnect()
        {
            OnPowerUpUnlock -= OnPowerUpUnlock;
        }

    }
}
