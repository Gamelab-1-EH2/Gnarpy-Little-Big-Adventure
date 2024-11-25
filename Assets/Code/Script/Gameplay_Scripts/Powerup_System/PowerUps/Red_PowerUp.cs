using Player.Model;

using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class Red_PowerUp : PowerUp
    {
        private float _coolDownTime;
        private float _usedFor;
        private bool _canRevert;

        bool _reverted;

        public Red_PowerUp(PlayerModel model) : base(model)
        {
            _canRevert = true;
            _reverted = false;
        }
        
        public override void Start()
        {
            if(!base._isUnlocked || !_canRevert)
                return;

            _reverted = !_reverted;

            _canRevert = false;
            _usedFor = 0f;
        }

        public override void Update()
        {
            if (!base._isUnlocked || !_reverted)
                return;

            if (_reverted)
            {
                Vector3 worldPos = base._playerModel.Movement.RigidBody.transform.position;
                float radious = base._playerModel.PowerUp.RedPowerUpRadious;

                Collider[] colliders = Physics.OverlapSphere(worldPos, radious, 1 << 8);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].TryGetComponent<MovableObject>(out MovableObject movable))
                        movable.ApplyGravity(Vector3.up, base._playerModel.PowerUp.RedPowerUpStrenght);
                }
            }

            UpdateProgresses();
        }

        private void UpdateProgresses()
        {
            if (_canRevert)
            {
                //Decrease CoolDown
                if (_playerModel.PowerUp.RedProgress > 0f)
                {
                    _coolDownTime -= Time.deltaTime;
                    _playerModel.PowerUp.RedProgress = _coolDownTime / _playerModel.PowerUp.RedDelay;
                }
                else
                    _canRevert = false;
            }
            else
            {
                //Increase Usage
                if (_playerModel.PowerUp.RedProgress < 1)
                {
                    _usedFor += Time.deltaTime;
                    _playerModel.PowerUp.RedProgress = _usedFor / _playerModel.PowerUp.RedDelay;
                }
                else
                {
                    _canRevert = true;
                    _playerModel.PowerUp.RedProgress = 1;
                    _coolDownTime = _playerModel.PowerUp.RedDelay;
                    base._playerModel.PowerUp.ShieldTransform.gameObject.SetActive(false);
                }
            }
        }

    }
}
