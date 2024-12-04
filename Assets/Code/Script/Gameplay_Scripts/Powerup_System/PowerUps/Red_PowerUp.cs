using Audio_System.SFX;
using Player.Model;

using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Red_PowerUp : PowerUp
    {
        private float _coolDownTime;

        private bool _onCoolDown;
        private bool _reverted;
        public Red_PowerUp(PlayerModel model) : base(model)
        {
            _onCoolDown = false;
            _reverted = false;
            _coolDownTime = 0f;
        }

        public override void Start()
        {
            if (!base._isUnlocked)
                return;

            if (_onCoolDown)
                return;
            
            _reverted = !_reverted;

            SFX sfx = _reverted ? _playerModel.AudioModel.ShieldOffSFX : _playerModel.AudioModel.ShieldOnSFX;
            SFXManager.PlaySFX?.Invoke(sfx, _playerModel.Movement.RigidBody.position);

            _onCoolDown = true;
            _coolDownTime = base._playerModel.PowerUp.RedDelay;

            if (_reverted)
                _playerModel.Rotation = Vector3.up;
            else
                _playerModel.Rotation = Vector3.down;
        }

        public override void Process()
        {
            if(_onCoolDown)
            {
                _coolDownTime -= Time.deltaTime;
                _playerModel.PowerUp.RedProgress = _coolDownTime / _playerModel.PowerUp.RedDelay;
                _playerModel.PowerUp.RedProgress = Mathf.Clamp01(_playerModel.PowerUp.RedProgress);

                if (_coolDownTime <= 0f)
                    _onCoolDown = false;
            }

            if(!_reverted)
                return;

            Vector3 worldPos = base._playerModel.Movement.RigidBody.transform.position;
            float radious = base._playerModel.PowerUp.RedPowerUpRadious;

            Collider[] colliders = Physics.OverlapSphere(worldPos, radious, 1 << 8);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent<MovableObject>(out MovableObject movable))
                    movable.ApplyGravity(Vector3.up, base._playerModel.PowerUp.RedPowerUpStrenght);
            }
        }
    }
}
