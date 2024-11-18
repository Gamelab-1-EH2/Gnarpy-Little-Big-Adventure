using UnityEngine;
using Audio_System.SFX;

[CreateAssetMenu(fileName = "New Player SO", menuName = "Settings/Player")]
public class Player_SO : ScriptableObject
{
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
    [Header("SFX")]
    [SerializeField] private SFX_SO _jumpSFX;
    [SerializeField] private SFX_SO _shootSFX;

    public SFX JumpSFX => _jumpSFX.GetSFX();
    public SFX ShootSFX => _shootSFX.GetSFX();
}
