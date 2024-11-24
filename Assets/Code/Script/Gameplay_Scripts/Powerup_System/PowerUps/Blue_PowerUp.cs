using Player.Model;
using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Blue_PowerUp : PowerUp
    {
        private float _lastTimeShoot;
        private PowerUpModel _powerUpModel;
        private ObjectPooler _pooler;

        public Blue_PowerUp(PlayerModel model) : base(model)
        {
            _lastTimeShoot = 0f;
            _powerUpModel = model.PowerUp;
            _pooler = new ObjectPooler(_powerUpModel.BlueProjectile, 15);
        }

        public override void Start()
        {
            if (!base._isUnlocked)
                return;

            float enlapsed = Time.time - _lastTimeShoot;

            if (enlapsed >= _powerUpModel.ShootDelay)
            {
                _lastTimeShoot = Time.time;

                Projectile projectile = _pooler.PoolObject().GetComponent<Projectile>();
                
                Vector3 shootDir = _powerUpModel.ProjectileDirection;
                shootDir.y *= _playerModel.Movement.LookingRight ? 1f : -1f;

                Vector3 offset = (Vector3)_powerUpModel.ProjectileOffset;
                offset.x *= _playerModel.Movement.LookingRight ? 1f : -1f;

                Vector3 projectilePos = base._playerModel.Movement.RigidBody.transform.position + offset;
                
                projectile.transform.position = projectilePos;
                projectile.Shoot(shootDir, _powerUpModel.ProjectileSpeed);
                projectile.gameObject.SetActive(true);
            }
        }

        public override void Update()
        {
            return;
        }
    }
}
