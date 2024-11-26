using UnityEngine;

namespace Player.Model
{
    public class FallModel
    {
        private Vector3 _gravity;
        private float _fallSpeedScalar;

        public FallModel(Player_SO playerSO)
        {
            _gravity = playerSO.Gravity;
            _fallSpeedScalar = playerSO.FallScalar;
        }

        public float FallScalar => _fallSpeedScalar;
        
        public Vector3 Gravity
        {
            get => _gravity;
            set => _gravity = value;
        }
    }
}
