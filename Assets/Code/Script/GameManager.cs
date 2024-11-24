using Player;
using System;
using Turret_System;
using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _gameManager;
        
        public static Action<GameState> OnGameStateChange;
        private GameState _gameState;

        [SerializeField] private DroppableManager _droppableManager;
        [SerializeField] private TurretManager _turretManager;

        private FallableManager _fallableManager = null;
        
        private void Awake()
        {
            if(_gameManager == null)
                _gameManager = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }

            _fallableManager = new FallableManager();
            _gameState = GameState.Menu;

            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            ChangeState(GameState.Gameplay);
        }

        private void FixedUpdate()
        {
            if(_gameState == GameState.Gameplay)
            {
                _fallableManager.FixedUpdate();
                _turretManager.FixedUpdate();
            }
        }

        private void ChangeState(GameState state)
        {
            switch (state)
            {
                case GameState.Gameplay:
                    if(_gameState == GameState.Menu)
                    {
                        FindObjectOfType<PlayerController>().OnPlayerDeath += Defeat;
                        _droppableManager.Start();
                        _fallableManager.Start();
                        _turretManager.Start();
                    }
                    break;

                case GameState.Menu:
                    
                    break;

                case GameState.Pause:

                    break;
            }

            _gameState = state;
            OnGameStateChange?.Invoke(_gameState);
        }

        private void Defeat() => ChangeState(GameState.Defeat);
    }
}
