#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

using Player.View;
using Player.Model;
using Player.Behaviour.Machine;
using Player.Behaviour.States;
using Collectible_System.PowerUp;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player_SO _playerSO;
        [SerializeField] private Transform _shieldTransform;

        private PlayerStateMachine _stateMachine;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody _rigidbody;

        private PowerUpController _powerUpController;

        //MVC
        public PlayerModel PlayerModel { get; private set; }
        private PlayerView _view;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            //Initialize Model
            PlayerModel = new PlayerModel(_playerSO, _rigidbody, _shieldTransform);

            //Initialize View
            Animator animator = GetComponent<Animator>();
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _view = new PlayerView(animator, spriteRenderer);

            _stateMachine = new PlayerStateMachine(new PlayerIdle_State(PlayerModel));

            _powerUpController = new PowerUpController(PlayerModel);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PowerUp_Collectible>(out PowerUp_Collectible powerup))
            {
                _spriteRenderer.color = powerup.Color;
                _powerUpController.UnlockPowerUp(powerup.PowerupType);
                powerup.Collect();
            }

            _stateMachine.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _stateMachine.OnTriggerExit(other);
        }

        private void FixedUpdate()
        {
            HandleState();
            HandlePowerUp();
            UpdateView();
        }

        private void HandleState()
        {
            _stateMachine.Process();
        }

        private void UpdateView()
        {
            _view.SetDirection(PlayerModel.Movement.Direction);
            _view.PlayAnimation(PlayerModel.State);
        }

        private void HandlePowerUp()
        {
            _powerUpController.Process();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.forward, _playerSO.RedPowerUpRadius);

            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position + _playerSO.GreenPowerUpOffset, Vector3.forward, _playerSO.GreenPowerUpRadius);
        }
#endif
    }
}
