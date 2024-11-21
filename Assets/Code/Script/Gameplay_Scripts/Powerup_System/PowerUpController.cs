using UnityEngine;
using UnityEngine.InputSystem;

using Player.Model;

namespace Collectible_System.PowerUp
{
    public class PowerUpController
    {
        private Red_PowerUp _powRed;
        private Blue_PowerUp _powBlue;
        private Green_PowerUp _powGreen;

        public PowerUpController(PlayerModel playerModel)
        {
            _powRed = new Red_PowerUp(playerModel);
            _powBlue = new Blue_PowerUp(playerModel);
            _powGreen = new Green_PowerUp(playerModel);

            InputManager.ActionMap.Gameplay.Powerup_1.started += EnterRed;
            InputManager.ActionMap.Gameplay.Powerup_2.started += EnterBlue;
            InputManager.ActionMap.Gameplay.Powerup_3.started += EnterGreen;
        }

        public void Process()
        {
            _powRed.Update();
            _powBlue.Update();
            _powGreen.Update();
        }

        public void UnlockPowerUp(PowerUpType type)
        {
            switch(type)
            {
                case PowerUpType.RED_STRAWBERRY:
                    _powRed.Unlocked = true;
                    break;

                case PowerUpType.BLUE_STRAWBERRY:
                    _powBlue.Unlocked = true;
                    break;

                case PowerUpType.GREEN_STRAWBERRY:
                    _powGreen.Unlocked = true;
                    break;
            }
        }
        
        public void LockPowerUp(PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.RED_STRAWBERRY:
                    _powRed.Unlocked = false;
                    break;

                case PowerUpType.BLUE_STRAWBERRY:
                    _powBlue.Unlocked = false;
                    break;

                case PowerUpType.GREEN_STRAWBERRY:
                    _powGreen.Unlocked = false;
                    break;
            }
        }

        private void EnterRed(InputAction.CallbackContext _) => EnterRed();
        private void EnterRed() => _powRed.Start();

        private void EnterBlue(InputAction.CallbackContext _) => EnterBlue();
        private void EnterBlue() => _powBlue.Start();

        private void EnterGreen(InputAction.CallbackContext _) => EnterGreen();
        private void EnterGreen() => _powGreen.Start();
    }
}
