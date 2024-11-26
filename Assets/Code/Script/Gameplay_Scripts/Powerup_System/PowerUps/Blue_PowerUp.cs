using Player.Model;

using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Blue_PowerUp : PowerUp
    {
        private PowerUpModel _powerUpModel;
        private ObjectPooler _pooler;

        private float _usedFor;
        private bool _inCooldown;
        private float _coolDownTime;

        public Blue_PowerUp(PlayerModel model) : base(model)
        {
            _powerUpModel = model.PowerUp;
            _pooler = new ObjectPooler(_powerUpModel.BlueProjectile, 15);
        }

        public override void Start()
        {
            if (!base._isUnlocked || _inCooldown)
                return;
            
            Projectile projectile = _pooler.PoolObject().GetComponent<Projectile>();

            Vector3 shootDir = _powerUpModel.ProjectileDirection;
            shootDir.y *= _playerModel.Movement.LookingRight ? 1f : -1f;

            Vector3 offset = (Vector3)_powerUpModel.ProjectileOffset;
            offset.x *= _playerModel.Movement.LookingRight ? 1f : -1f;

            Vector3 projectilePos = base._playerModel.Movement.RigidBody.transform.position + offset;

            projectile.transform.position = projectilePos;
            projectile.Shoot(shootDir, _powerUpModel.ProjectileSpeed);
            projectile.gameObject.SetActive(true);
            
            _coolDownTime = _playerModel.PowerUp.ShootDelay;
            _playerModel.PowerUp.BlueProgress = 1f;
            _inCooldown = true;
        }

        public override void Process()
        {
            if (_inCooldown)
            {
                //Decrease Cooldown
                if(_playerModel.PowerUp.BlueProgress > 0f)
                {
                    _coolDownTime -= Time.deltaTime;
                    _playerModel.PowerUp.BlueProgress = _coolDownTime / _playerModel.PowerUp.ShootDelay;
                }
                else
                    _inCooldown = false;
            }
        }

    }
}
