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
        private float _redDelayProgress;

        public Action<float> OnGreenProgressChanged;
        private float _greenPowerUpStrenght;
        private float _greenDelay;
        private float _greenDuration;
        private float _greenDelayProgress;
        private Shield _shield;

        public Action<float> OnBlueProgressChanged;
        private GameObject _bluePowerUpProjectile;
        private float _projectileSpeed;
        private Vector3 _projectileOffset;
        private Vector2 _projectileDirection;
        private float _blueShootDelay;
        private float _blueDelayProgress;

        public PowerUpModel(Player_SO playerSO, Shield shield)
        {
            _redPowerUpRadious = playerSO.RedPowerUpRadius;
            _redPowerUpStrenght = playerSO.RedPowerUpStrenght;
            _redDelay = playerSO.RedPowerUpDelay;

            _greenPowerUpStrenght = playerSO.GreenPowerUpStrenght;
            _greenDelay = playerSO.GreenPowerUpDelay;
            _greenDuration = playerSO.GreenPowerUpDuration;
            _shield = shield;

            _bluePowerUpProjectile = playerSO.BluePowerUpProjectile;
            _projectileSpeed = playerSO.ProjectileSpeed;
            _projectileDirection = playerSO.ShootDirection;
            _projectileOffset = playerSO.ShootOffset;
            _blueShootDelay = playerSO.ShootDelay;
        }

        public float RedPowerUpRadious => _redPowerUpRadious;
        public float RedPowerUpStrenght => _redPowerUpStrenght;
        public float RedDelay => _redDelay;
        public float RedProgress
        {
            get => _redDelayProgress;
            set
            {
                _redDelayProgress = value;
                OnRedProgressChanged?.Invoke(value);
            }
        }

        public float GreenPowerUpStrenght => _greenPowerUpStrenght;
        public Shield Shield => _shield;
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
