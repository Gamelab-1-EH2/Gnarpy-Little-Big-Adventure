using UnityEngine;

using Player.View;
using Player.Model;
using Player.Behaviour.Machine;
using Player.Behaviour.States;

namespace Player
{
    [RequireComponent(typeof (Rigidbody), typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player_SO _playerSO;

        private PlayerStateMachine _stateMachine;
        private Rigidbody _rigidbody;

        //MVC
        private PlayerModel _model;
        private PlayerView _view;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            //Initialize Model
            _model = new PlayerModel(_playerSO, _rigidbody);

            //Initialize View
            Animator animator = GetComponent<Animator>();
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _view = new PlayerView(animator, spriteRenderer);

            _stateMachine = new PlayerStateMachine(new PlayerIdle_State(_model));
        }

        private void OnTriggerEnter(Collider other)
        {
            _stateMachine.OnTriggerEnter(other);
            
            //Damaged state:
            //_stateMachine.PushState(State);
        }

        private void OnTriggerExit(Collider other)
        {
            _stateMachine.OnTriggerExit(other);
        }

        private void FixedUpdate()
        {
            HandleState();
            UpdateView();
        }

        private void HandleState()
        {
            _stateMachine.Process();
        }

        private void UpdateView()
        {
            _view.SetDirection(_model.Movement.Direction);
            _view.PlayAnimation(_model.State);
        }
    }
}
