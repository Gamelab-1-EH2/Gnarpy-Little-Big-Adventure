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
        [SerializeField] private bool _isDebug = false;

        private GameManager_Model _model;
        private GameManager_StateMachine _stateMachine;

        private void Awake()
        {
            _model = new GameManager_Model(_droppableManager, _turretManager, _gameSceneManager);
            _model.SetManagerTransform(this.transform);

            _model.GameState = _initialState;
            _model.OnStateChanged += GameStateChanged;
        }

        private void Start()
        {
            if(_isDebug)
            {
                if (_initialState == GameState_Type.Gameplay)
                    _stateMachine = new GameManager_StateMachine(new GameState_Gameplay(_model));
            }
            else
            {
                switch(_initialState)
                {
                    case GameState_Type.Gameplay:
                        string gameScene = _model.SceneManager.GameScenes[0];
                        _stateMachine = new GameManager_StateMachine(new GameState_Loading(_model, new GameState_Gameplay(_model), gameScene));
                        break;

                    case GameState_Type.Pause:
                        _stateMachine = new GameManager_StateMachine(new GameState_Pause(_model));
                        break;

                    case GameState_Type.Menu:
                        _stateMachine = new GameManager_StateMachine(new GameState_Menu(_model));
                        break;

                    case GameState_Type.Defeat:
                        _stateMachine = new GameManager_StateMachine(new GameState_Defeat(_model));
                        break;

                    case GameState_Type.Victory:
                        //_stateMachine = new GameManager_StateMachine(new GameState_Victory(_model));
                        break;

                    case GameState_Type.Loading:
                        _stateMachine = new GameManager_StateMachine(new GameState_Menu(_model));
                        break;
                }
            }
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
