using Player.Model;
using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Green_PowerUp : PowerUp
    {
        private SphereCollider _shieldCollider;

        private float _coolDownTime;
        private float _usedFor;
        private bool _inCooldown;

        public Green_PowerUp(PlayerModel model) : base(model)
        {

        }

        public override void Start()
        {
            if (!base._isUnlocked || base._isBeingUsed)
                return;

            base._isBeingUsed = true;
            base._playerModel.PowerUp.ShieldTransform.gameObject.SetActive(true);
            _inCooldown = false;
            _usedFor = 0f;
        }

        public override void Update()
        {
            if (!base._isUnlocked || !base._isBeingUsed)
                return;

            if (_inCooldown)
            {
                if (_playerModel.PowerUp.GreenProgress > 0f)
                {
                    _coolDownTime -= Time.deltaTime;
                    _playerModel.PowerUp.GreenProgress = _coolDownTime / _playerModel.PowerUp.GreenDelay;
                }
                else
                { 
                    _inCooldown = false;
                    base._isBeingUsed = false;
                }
            }
            else
            {
                if (_playerModel.PowerUp.GreenProgress < 1)
                {
                    Vector3 shieldPos = base._playerModel.Movement.RigidBody.transform.position;
                    shieldPos += base._playerModel.PowerUp.GreenPowerUpOffset;

                    float radious = base._playerModel.PowerUp.GreenPowerUpRadious;

                    Collider[] colliders = Physics.OverlapSphere(shieldPos, radious, 1 << 8);
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].TryGetComponent<MovableObject>(out MovableObject movable))
                        {
                            Vector3 opposideDirection = movable.transform.position - shieldPos;
                            movable.Deflect(opposideDirection, base._playerModel.PowerUp.GreenPowerUpStrenght);
                        }
                    }

                    _usedFor += Time.deltaTime;
                    _playerModel.PowerUp.GreenProgress = _usedFor / _playerModel.PowerUp.GreenDuration;
                }
                else
                {
                    _inCooldown = true;
                    _playerModel.PowerUp.GreenProgress = 1;
                    _coolDownTime = _playerModel.PowerUp.GreenDelay;
                    base._playerModel.PowerUp.ShieldTransform.gameObject.SetActive(false);
                }
            }
        }
    }
}
