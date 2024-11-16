using UnityEngine;

namespace Player.Model
{
    public class MovementModel
    {
        private bool _canMove;
        private float _movementSpeed;
        private float _speedMultiplier;
        
        private float _longJumpForce;
        private float _shortJumpForce;
        private float _longJumpTime;

        private float _groundCheckDistance;
        private float _groundCheckOffset;
        private float _fallSpeedScalar;

        private Vector3 _direction;
        private Rigidbody _rigidbody;

        public MovementModel(Player_SO playerSO, Rigidbody rigidbody)
        {
            _movementSpeed = playerSO.MovementSpeed;
            _speedMultiplier = 1f;

            _longJumpForce = playerSO.LongJumpForce;
            _shortJumpForce = playerSO.ShortJumpForce;
            _fallSpeedScalar = playerSO.FallScalar;
            _longJumpTime = playerSO.LongJumpHoldTime;

            _groundCheckDistance = playerSO.GroundCheckDistance;
            _groundCheckOffset = playerSO.GroundCheckOffsetY;

            _rigidbody = rigidbody;
            _canMove = true;
        }

        public float SpeedMultiplier
        {
            get => _speedMultiplier;
            set => _speedMultiplier = value;
        }

        public float Speed
        {
            get => _speedMultiplier * _movementSpeed;
            set => _movementSpeed = value;
        }

        public float GetJumpForce(float jumpProgress) => Mathf.Lerp(_shortJumpForce, _longJumpForce, jumpProgress);
        public float LongJumpTime => _longJumpTime;
        public float FallScalar => _fallSpeedScalar;

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }

        public Rigidbody Body
        {
            get => _rigidbody;
            set => _rigidbody = value;
        }

        public float GroundCheckDistance => _groundCheckDistance;
        public float GroundCheckOffset => _groundCheckOffset;
    }
}
