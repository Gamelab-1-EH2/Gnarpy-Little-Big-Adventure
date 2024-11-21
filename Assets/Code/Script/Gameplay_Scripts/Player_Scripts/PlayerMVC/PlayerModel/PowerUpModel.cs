using UnityEngine;

namespace Player.Model
{
    public class PowerUpModel
    {
        private float _redPowerUpRadious;
        private float _redPowerUpStrenght;

        private float _greenPowerUpRadious;
        private float _greenPowerUpStrenght;
        private Vector3 _greenPowerUpOffset;

        public PowerUpModel(Player_SO playerSO)
        {
            _redPowerUpRadious = playerSO.RedPowerUpRadius;
            _redPowerUpStrenght = playerSO.RedPowerUpStrenght;

            _greenPowerUpRadious = playerSO.GreenPowerUpRadius;
            _greenPowerUpStrenght = playerSO.GreenPowerUpStrenght;
            _greenPowerUpOffset = playerSO.GreenPowerUpOffset;
        }

        public float RedPowerUpRadious => _redPowerUpRadious;
        public float RedPowerUpStrenght => _redPowerUpStrenght;

        public float GreenPowerUpRadious => _greenPowerUpRadious;
        public float GreenPowerUpStrenght => _greenPowerUpStrenght;
        public Vector3 GreenPowerUpOffset => _greenPowerUpOffset;
    }
}
