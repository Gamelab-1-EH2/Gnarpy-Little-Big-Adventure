using UnityEngine;

namespace Collectible_System.PowerUp
{
    public class PowerUp_Collectible : Collectible
    {
        //[SerializeField] private Color _powerupColor = Color.white;
        //public Color Color => _powerupColor;
        public PowerUpType GetPowerUpType()
        {
            switch (base.CollectibleType)
            {
                case CollectibleType.GreenPowerUp:
                    return PowerUpType.GREEN_STRAWBERRY;

                case CollectibleType.BluePowerUp:
                    return PowerUpType.BLUE_STRAWBERRY;

                case CollectibleType.RedPowerUp:
                    return PowerUpType.RED_STRAWBERRY;
            }

            return 0;
        }
    }
}
