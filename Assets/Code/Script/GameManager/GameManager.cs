using System;
using UnityEngine;

using Turret_System;
using GameManagement.Model;
using GameManagement.Behaviour;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static Action<GameState_Type> OnGameStateChange;

        [SerializeField] private DroppableManager _droppableManager;
        [SerializeField] private TurretManager _turretManager;

        private GameManager_Model _model;
        private GameManager_StateMachine _stateMachine;

        private void Awake()
        {
            _model = new GameManager_Model(_droppableManager, _turretManager);
            _model.SetManagerTransform(this.transform);
            _model.GameState = GameState_Type.Menu;
            _model.OnStateChanged += GameStateChanged;
        }

        private void Start()
        {
            _stateMachine = new GameManager_StateMachine(new GameState_Gameplay(_model));
        }

        private void FixedUpdate()
        {
            _stateMachine.Process();
        }

        private void GameStateChanged(GameState_Type newState)
        {
            Debug.Log($"New Game State: {newState}");
            OnGameStateChange?.Invoke(newState);
        }
    }
}
