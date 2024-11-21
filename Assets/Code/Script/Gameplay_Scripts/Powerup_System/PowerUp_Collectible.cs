using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class PowerUp_Collectible : Collectible
    {
        [SerializeField] private Color _powerupColor = Color.white;
        [SerializeField] private PowerUpType _powerupType = PowerUpType.RED_STRAWBERRY;

        public Color Color => _powerupColor;
        public PowerUpType PowerupType => _powerupType;
    }
}
