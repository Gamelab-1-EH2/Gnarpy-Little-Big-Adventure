using UnityEngine;
using GameManagement;
using System;

namespace UI_System
{
    public class UIManager : MonoBehaviour
    {
        public Action OnNewGameRequest;

        private GameplayUI _gameUI;


        private void Awake()
        {
            _gameUI = GetComponentInChildren<GameplayUI>(true);
            GameManager.OnGameStateChange += UpdateUITab;
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChange -= UpdateUITab;
        }

        private void UpdateUITab(GameState_Type gameState)
        {
            switch(gameState)
            {
                case GameState_Type.Gameplay:
                    _gameUI.SetToGame();
                    break;

                case GameState_Type.Pause:
                    _gameUI.SetToPause();
                    break;

                case GameState_Type.Menu:
                    _gameUI.gameObject.SetActive(false);
                    break;

                case GameState_Type.Defeat:
                    _gameUI.gameObject.SetActive(false);
                    break;

                case GameState_Type.Victory:
                    _gameUI.gameObject.SetActive(false);
                    break;
            }
        }
    }
}


/*
Gameplay = 0,
Pause = 1,
Menu = 2,
Defeat = 4,
Victory = 8,
*/
