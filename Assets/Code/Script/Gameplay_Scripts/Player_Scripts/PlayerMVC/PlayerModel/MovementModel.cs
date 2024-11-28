using UnityEngine;

namespace Player.Model
{
    public class MovementModel
    {
        private bool _canMove;
        private float _movementSpeed;
        private float _speedMultiplier;

        private JumpModel _jumpModel;
        private FallModel _fallModel;

        private float _groundCheckDistance;
        private float _groundCheckOffset;

        private Vector3 _direction;
        private Vector3 _lastAllowedPos;
        private Rigidbody _rigidbody;

        private bool _lookingRight;

        public MovementModel(Player_SO playerSO, Rigidbody rigidbody)
        {
            _movementSpeed = playerSO.MovementSpeed;
            _speedMultiplier = 1f;

            _groundCheckDistance = playerSO.GroundCheckDistance;
            _groundCheckOffset = playerSO.GroundCheckOffsetY;

            _rigidbody = rigidbody;
            _canMove = true;

            _lookingRight = true;

            _jumpModel = new JumpModel(playerSO);
            _fallModel = new FallModel(playerSO);
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

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        public Vector3 Direction
        {
            get => _direction;
            set
            {
                if (Direction.x > 0)
                    _lookingRight = true;
                else if(Direction.x < 0)
                    _lookingRight = false;

                _direction = value;
            }
        }

        public Vector3 LastAllowedPosition
        {
            get => _lastAllowedPos;
            set => _lastAllowedPos = value;
        }

        public Rigidbody RigidBody
        {
            get => _rigidbody;
            set => _rigidbody = value;
        }

        public bool LookingRight => _lookingRight;

        public JumpModel Jump => _jumpModel;
        public FallModel Fall => _fallModel;

        public float GroundCheckDistance => _groundCheckDistance;
        public float GroundCheckOffset => _groundCheckOffset;
    }
}
