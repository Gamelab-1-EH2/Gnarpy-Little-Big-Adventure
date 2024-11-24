using System;
using UnityEngine;

using Collectible_System.PowerUp;

namespace Player.Model
{
    public class PowerUpModel
    {
        public Action<PowerUpType> OnPowerUpUnlock;

        public Action<float> OnRedDelayProgressChanged;        
        private float _redPowerUpRadious;
        private float _redPowerUpStrenght;
        private float _redDelay;
        private float _redDelayProgress;

        public Action<float> OnGreenDelayProgressChanged;
        private float _greenPowerUpRadious;
        private float _greenPowerUpStrenght;
        private Vector3 _greenPowerUpOffset;
        private Transform _shieldTransform;
        private float _greenDelay;
        private float _greenDelayProgress;

        public Action<float> OnBlueDelayProgressChanged;
        private GameObject _bluePowerUpProjectile;
        private float _projectileSpeed;
        private float _projectileDelay;
        private Vector3 _projectileOffset;
        private Vector2 _projectileDirection;
        private float _blueShootDelay;
        private float _blueDelayProgress;

        public PowerUpModel(Player_SO playerSO, Transform shieldTransform)
        {
            _redPowerUpRadious = playerSO.RedPowerUpRadius;
            _redPowerUpStrenght = playerSO.RedPowerUpStrenght;
            _redDelay = playerSO.RedPowerUpDelay;

            _greenPowerUpRadious = playerSO.GreenPowerUpRadius;
            _greenPowerUpStrenght = playerSO.GreenPowerUpStrenght;
            _greenPowerUpOffset = playerSO.GreenPowerUpOffset;
            _greenDelay = playerSO.GreenPowerUpDelay;
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
        public float RedDelayProgress
        {
            get => _redDelayProgress;
            set
            {
                _redDelayProgress = value;
                OnRedDelayProgressChanged?.Invoke(value);
            }
        }

        public float GreenPowerUpRadious => _greenPowerUpRadious;
        public float GreenPowerUpStrenght => _greenPowerUpStrenght;
        public Vector3 GreenPowerUpOffset => _greenPowerUpOffset;
        public Transform ShieldTransform => _shieldTransform;
        public float GreenDelay => _greenDelay;
        public float GreenDelayProgress
        {
            get => _greenDelayProgress;
            set
            {
                _greenDelayProgress = value;
                OnGreenDelayProgressChanged?.Invoke(value);
            }
        }

        public GameObject BlueProjectile => _bluePowerUpProjectile;
        public float ProjectileSpeed => _projectileSpeed;
        public Vector3 ProjectileDirection => _projectileDirection;
        public Vector2 ProjectileOffset => _projectileOffset;
        public float ProjectileDelay => _projectileDelay;
        public float ShootDelay => _blueShootDelay;
        public float BlueDelayProgress
        {
            get => _blueDelayProgress;
            set
            {
                _blueDelayProgress = value;
                OnBlueDelayProgressChanged?.Invoke(value);
            }
        }

        public void UnlockPowerUp(PowerUpType powerUpType) => OnPowerUpUnlock?.Invoke(powerUpType);

    }
}
