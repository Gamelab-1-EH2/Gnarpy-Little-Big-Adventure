using UnityEngine;

using Player;
using Player.Model;
using Collectible_System.PowerUp;
using UI_System.Miscellaneous;

namespace UI_System
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _playerHealthBar;
        [SerializeField] private PowerUpBar _playerPowerUpBar;
        [SerializeField] private CollectibleBar _collectibleBar;
        [Space(15)]
        [SerializeField] private HealthBar _bossHealthBar;

        private void OnEnable()
        {
            PlayerModel playerModel = FindObjectOfType<PlayerController>()?.Model;
            if (playerModel == null)
                return;

            //Player HP
            _playerHealthBar.SetHealth(playerModel.HealthPoints);
            playerModel.OnHPChanged += _playerHealthBar.SetHealth;
            
            //Power Ups
            _playerPowerUpBar.LockPowerUp(PowerUpType.RED_STRAWBERRY);
            _playerPowerUpBar.LockPowerUp(PowerUpType.BLUE_STRAWBERRY);
            _playerPowerUpBar.LockPowerUp(PowerUpType.GREEN_STRAWBERRY);

            playerModel.PowerUp.OnPowerUpUnlock += _playerPowerUpBar.UnlockPowerUp;
            playerModel.PowerUp.OnRedProgressChanged += _playerPowerUpBar.UpdateRedCooldown;
            playerModel.PowerUp.OnBlueProgressChanged += _playerPowerUpBar.UpdateBlueCooldown;
            playerModel.PowerUp.OnGreenProgressChanged += _playerPowerUpBar.UpdateGreenCooldown;

            //Collectible
            _collectibleBar.SetCollected(0);
            playerModel.OnBallOfWoolCollected += _collectibleBar.SetCollected;

            //Boss HP
            HideBossHP();
        }

        private void OnDisable()
        {
            PlayerModel playerModel = FindObjectOfType<PlayerController>()?.Model;
            if (playerModel == null)
                return;

            playerModel.OnHPChanged -= _playerHealthBar.SetHealth;
            playerModel.PowerUp.OnPowerUpUnlock -= _playerPowerUpBar.UnlockPowerUp;
            playerModel.PowerUp.OnRedProgressChanged -= _playerPowerUpBar.UpdateRedCooldown;
            playerModel.PowerUp.OnBlueProgressChanged -= _playerPowerUpBar.UpdateBlueCooldown;
            playerModel.PowerUp.OnGreenProgressChanged -= _playerPowerUpBar.UpdateGreenCooldown;

            playerModel.OnBallOfWoolCollected -= _collectibleBar.SetCollected;
        }

        private void ShowBossHP() => _bossHealthBar.gameObject.SetActive(true);
        private void HideBossHP() => _bossHealthBar.gameObject.SetActive(false);
    }

}