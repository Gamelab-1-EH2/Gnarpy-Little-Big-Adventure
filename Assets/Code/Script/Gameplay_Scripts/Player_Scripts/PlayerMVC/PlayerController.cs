using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Player.View;
using Player.Model;
using Player.Behaviour.Machine;
using Player.Behaviour.States;
using Collectible_System.PowerUp;
using System;
using Collectible_System;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public Action OnPlayerDeath;

        [SerializeField] private Player_SO _playerSO;
        [SerializeField] private Transform _shieldTransform;

        private PlayerStateMachine _stateMachine;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody _rigidBody;

        private PowerUpController _powerUpController;
        
        //MVC
        public PlayerModel Model { get; private set; }
        private PlayerView _view;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            //Initialize Model
            Model = new PlayerModel(_playerSO, _rigidBody, _shieldTransform);

            //Initialize View
            Animator animator = GetComponent<Animator>();
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _view = new PlayerView(animator, spriteRenderer);

            _stateMachine = new PlayerStateMachine(new PlayerIdle_State(Model));

            _powerUpController = new PowerUpController(Model);
            Model.OnHPChanged += HealthDecreased;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PowerUp_Collectible>(out PowerUp_Collectible powerup))
            {
                //_spriteRenderer.color = powerup.Color; (Removed ColorChange ByEma)
                _powerUpController.UnlockPowerUp(powerup.GetPowerUpType());
                powerup.Collect();
            }
            else if (other.TryGetComponent<Collectible>(out Collectible collectible))
            {
                switch (collectible.CollectibleType)
                {
                    case CollectibleType.Ball_of_Wool:
                        Model.BallOfWool++;
                        collectible.Collect();
                        break;
                    case CollectibleType.Catnip:
                        if (Model.HealthPoints < _playerSO.HealthPoints)
                        {
                            Model.HealthPoints++;
                            collectible.Collect();
                        }
                        break;
                }
            }

            _stateMachine.OnTriggerEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            _stateMachine.OnTriggerExit(other);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<SpiderWeb>(out SpiderWeb spiderWeb))
            {
                _stateMachine.PushState(new PlayerJump_State(Model, true));
            }
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
            _view.SetDirection(Model.Movement.Direction);
            _view.SetRotation(Model.Rotation);
            _view.PlayAnimation(Model.State);
        }

        private void HandlePowerUp()
        {
            _powerUpController.Process();
        }

        public void Damage() => Model.HealthPoints -= 1;

        private void HealthDecreased(int hp)
        {
            if (hp <= 0 && Model.State != Player.Model.PlayerState.Dead)
            {
                _stateMachine.PushState(new PlayerDeath_State(Model));
                OnPlayerDeath?.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnPlayerDeath -= OnPlayerDeath;
            //Disconnect State Machine
            _stateMachine.PushState(null);
            Model.Disconnect();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.forward, _playerSO.RedPowerUpRadius);

            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position + _playerSO.GreenPowerUpOffset, Vector3.forward, _playerSO.GreenPowerUpRadius);

            if(Model != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(Model.Movement.LastAllowedPosition, 0.5f);
            }
        }
        #endif
    }
}
