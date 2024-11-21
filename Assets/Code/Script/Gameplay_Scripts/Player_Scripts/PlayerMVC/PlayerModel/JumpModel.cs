using UnityEngine;

namespace Player.Model
{
    public class JumpModel
    {
        private float _jumpForce;
        private float _jumpTime;
        private AnimationCurve _jumpCurve;

        public JumpModel(Player_SO playerSO)
        {
            _jumpForce = playerSO.JumpForce;
            _jumpTime = playerSO.JumpTime;
            _jumpCurve = playerSO.JumpSpeedCurve;
        }

        public float JumpForce => _jumpForce;
        public float JumpTime => _jumpTime;
        public AnimationCurve JumpCurve => _jumpCurve;

    }
}
