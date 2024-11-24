using System;
using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _gameManager;
        
        public static Action<GameState> OnGameStateChange;
        private GameState _gameState;

        [SerializeField] private DroppableManager _droppableManager;
        private FallableManager _fallableManager;
        
        private void Awake()
        {
            if(_gameManager == null)
                _gameManager = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            _droppableManager.Start();

            _fallableManager = new FallableManager();
            _fallableManager.Start();

            OnGameStateChange?.Invoke(GameState.Gameplay);
        }


        private void FixedUpdate()
        {
            if(_gameState == GameState.Gameplay)
            {
                _fallableManager.FixedUpdate();
            }
        }
    }
}
