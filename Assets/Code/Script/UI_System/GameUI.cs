using Player;
using Player.Model;

using Collectible_System.PowerUp;

using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private HealthBar _playerHealthBar;
    [SerializeField] private PowerUpBar _playerPowerUpBar;

    private void Start()
    {
        PlayerModel model = FindObjectOfType<PlayerController>().Model;

        _playerHealthBar.SetHealth(model.HealthPoints);
        model.OnHPChanged += _playerHealthBar.SetHealth;
        

        _playerPowerUpBar.LockPowerUp(PowerUpType.RED_STRAWBERRY);
        _playerPowerUpBar.LockPowerUp(PowerUpType.BLUE_STRAWBERRY);
        _playerPowerUpBar.LockPowerUp(PowerUpType.GREEN_STRAWBERRY);

        model.PowerUp.OnPowerUpUnlock += _playerPowerUpBar.UnlockPowerUp;
        model.PowerUp.OnRedDelayProgressChanged += _playerPowerUpBar.UpdateRedCooldown;
        model.PowerUp.OnBlueDelayProgressChanged += _playerPowerUpBar.UpdateBlueCooldown;
        model.PowerUp.OnGreenDelayProgressChanged += _playerPowerUpBar.UpdateGreenCooldown;
    }
}
