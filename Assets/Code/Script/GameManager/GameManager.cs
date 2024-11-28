using System;
using UnityEngine;

using Turret_System;
using GameManagement.Model;
using GameManagement.Behaviour;
using GameManagement.GameSceneManagement;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static Action<GameState_Type> OnGameStateChange;

        [SerializeField] private DroppableManager _droppableManager;
        [SerializeField] private TurretManager _turretManager;
        [SerializeField] private GameSceneManager _gameSceneManager;
        [SerializeField] private GameState_Type _initialState = GameState_Type.Menu;

        [SerializeField] private bool _debug = false;

        private GameManager_Model _model;
        private GameManager_StateMachine _stateMachine;

        private void Awake()
        {
            _model = new GameManager_Model(_droppableManager, _turretManager, _gameSceneManager);
            _model.SetManagerTransform(this.transform);
            _model.GameState = GameState_Type.Menu;
            _model.OnStateChanged += GameStateChanged;

            if(!_debug)
                _model.SceneManager.LoadGameScene(0);
        }

        private void Start()
        {

            if(_initialState == GameState_Type.Gameplay)
                _stateMachine = new GameManager_StateMachine(new GameState_Gameplay(_model));
            else if (_initialState == GameState_Type.Menu)
                _stateMachine = new GameManager_StateMachine(new GameState_Menu(_model));
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
