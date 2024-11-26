using Collectible_System.PowerUp;

using UnityEngine;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour
{
    [SerializeField] private PowerUpUIData _redPowerUp;
    [SerializeField] private PowerUpUIData _bluePowerUp;
    [SerializeField] private PowerUpUIData _greenPowerUp;
    
    public void UpdateRedCooldown(float progress) => _redPowerUp.SetCoolDown(progress);
    public void UpdateBlueCooldown(float progress) => _bluePowerUp.SetCoolDown(progress);
    public void UpdateGreenCooldown(float progress) => _greenPowerUp.SetCoolDown(progress);
    
    public void LockPowerUp(PowerUpType powerUpType)
    {
        switch (powerUpType)
        {
            case PowerUpType.RED_STRAWBERRY:
                _redPowerUp.LockPowerUp();
                break;

            case PowerUpType.BLUE_STRAWBERRY:
                _bluePowerUp.LockPowerUp();
                break;

            case PowerUpType.GREEN_STRAWBERRY:
                _greenPowerUp.LockPowerUp();
                break;
        }
    }

    public void UnlockPowerUp(PowerUpType powerUpType)
    {
        switch(powerUpType)
        {
            case PowerUpType.RED_STRAWBERRY:
                _redPowerUp.UnlockPowerUp();
                break;

            case PowerUpType.BLUE_STRAWBERRY:
                _bluePowerUp.UnlockPowerUp();
                break;

            case PowerUpType.GREEN_STRAWBERRY:
                _greenPowerUp.UnlockPowerUp();
                break;
        }
    }
}

[System.Serializable]
public class PowerUpUIData
{
    [SerializeField] private Image _powerUpImage;
    [SerializeField] private Image _powerUpCooldowImage;
    [SerializeField] private Sprite _lockedPowerUp;
    [SerializeField] private Sprite _unlockedPowerUp;

    public Image PowerUpImage
    {
        get => _powerUpImage;
        set => _powerUpImage = value;
    }

    public void LockPowerUp() => _powerUpImage.sprite = _lockedPowerUp;
    public void UnlockPowerUp() => _powerUpImage.sprite = _unlockedPowerUp;
    public void SetCoolDown(float progress) => _powerUpCooldowImage.fillAmount = progress;
}
