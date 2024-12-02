using UnityEngine;
using Audio_System.SFX;

[CreateAssetMenu(fileName = "New Player SO", menuName = "Settings/Player")]
public class Player_SO : ScriptableObject
{
    [SerializeField] public int HealthPoints = 3;
    [SerializeField] public float MaxY = 25f;
    [SerializeField] public float MinY = -10f;
    [Header("Movement")]
    [SerializeField] public float MovementSpeed = 25f;
    [Header("Jump")]
    [SerializeField] public float JumpForce = 25f;
    [SerializeField] public AnimationCurve JumpSpeedCurve;
    [SerializeField] public float JumpTime = 1f;
    [SerializeField] public float FallScalar = 0.25f;
    [SerializeField] public Vector3 Gravity = Vector3.down * 75.5f;
    [Space(5f)]
    [SerializeField, Min(0f)] public float GroundCheckDistance = 0.1f;
    [SerializeField] public float GroundCheckOffsetY = -0.5f;
    [Space(15f)]
    [Header("Power Ups")]
    [SerializeField] public float RedPowerUpRadius = 5f;
    [SerializeField] public float RedPowerUpStrenght = 15f;
    [SerializeField] public float RedPowerUpDelay = 15f;
    [Space(10f)]
    [SerializeField] public float GreenPowerUpStrenght = 7f;
    [SerializeField] public float GreenPowerUpDelay = 20f;
    [SerializeField] public float GreenPowerUpDuration = 15f;
    [Space(10f)]
    [SerializeField] public GameObject BluePowerUpProjectile;
    [SerializeField] public float ProjectileSpeed = 15f;
    [SerializeField] public float ShootDelay = 1f;
    [SerializeField] public Vector3 ShootDirection = new Vector3(0f, 0f, 90f);
    [SerializeField] public Vector2 ShootOffset = new Vector2(0.5f, 0.5f);
    [Header("SFX")]
    [SerializeField] private SFX_SO _jumpSFX;
    [SerializeField] private SFX_SO _shootSFX;

    public SFX JumpSFX => _jumpSFX.GetSFX();
    public SFX ShootSFX => _shootSFX.GetSFX();
}
