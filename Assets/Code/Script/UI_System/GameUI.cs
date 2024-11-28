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
        [Space(15)]
        [SerializeField] private HealthBar _bossHealthBar;

        private void Start()
        {
            PlayerModel playerModel = FindObjectOfType<PlayerController>().Model;

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

            //Boss HP
            HideBossHP();
        }

        private void ShowBossHP() => _bossHealthBar.gameObject.SetActive(true);
        private void HideBossHP() => _bossHealthBar.gameObject.SetActive(false);
    }

}