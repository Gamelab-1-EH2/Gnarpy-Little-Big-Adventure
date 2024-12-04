using Audio_System.SFX;
using Player.Model;
using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Green_PowerUp : PowerUp
    {
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
            base._playerModel.PowerUp.Shield.gameObject.SetActive(true);
            _inCooldown = false;
            _usedFor = 0f;
        }

        public override void Process()
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
                    SFXManager.PlaySFX?.Invoke(_playerModel.AudioModel.ShieldOnSFX, _playerModel.Movement.RigidBody.position);
                }
            }
            else
            {
                if (_playerModel.PowerUp.GreenProgress < 1)
                {
                    _playerModel.PowerUp.Shield.gameObject.SetActive(true);

                    _usedFor += Time.deltaTime;
                    _playerModel.PowerUp.GreenProgress = _usedFor / _playerModel.PowerUp.GreenDuration;
                }
                else
                {
                    _inCooldown = true;
                    _playerModel.PowerUp.GreenProgress = 1;
                    _coolDownTime = _playerModel.PowerUp.GreenDelay;
                    base._playerModel.PowerUp.Shield.gameObject.SetActive(false);
                    SFXManager.PlaySFX?.Invoke(_playerModel.AudioModel.ShieldOnSFX, _playerModel.Movement.RigidBody.position);
                }
            }
        }
    }
}
