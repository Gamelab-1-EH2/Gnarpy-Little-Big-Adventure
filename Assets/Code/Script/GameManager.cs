using System;
using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public static Action<GameState> OnGameStateChange;
        private static GameManager _gameManager;
        
        private GameState _gameState;
        
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
            OnGameStateChange?.Invoke(GameState.Gameplay);
        }
    }
}
