using UnityEngine;
using GameManagement;
using System;

namespace UI_System
{
    public class UIManager : MonoBehaviour
    {
        public Action OnNewGameRequest;

        private GameplayUI _gameUI;
        private MenuUI _menuUI;
        private LoadingUI _loadingUI;

        private void Awake()
        {
            _gameUI = GetComponentInChildren<GameplayUI>(true);
            _menuUI = GetComponentInChildren<MenuUI>(true);
            _loadingUI = GetComponentInChildren<LoadingUI>(true);

            GameManager.OnGameStateChange += UpdateUITab;
            UpdateUITab(GameState_Type.Menu);
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
                    _gameUI.gameObject.SetActive(true);
                    _gameUI.SetToGame();
                    _menuUI.gameObject.SetActive(false);
                    _loadingUI.gameObject.SetActive(false);
                    break;

                case GameState_Type.Pause:
                    _gameUI.gameObject.SetActive(true);
                    _gameUI.SetToPause();
                    _menuUI.gameObject.SetActive(false);
                    _loadingUI.gameObject.SetActive(false);
                    break;

                case GameState_Type.Menu:
                    _gameUI.gameObject.SetActive(false);
                    _menuUI.gameObject.SetActive(true);
                    _loadingUI.gameObject.SetActive(false);
                    break;

                case GameState_Type.Defeat:
                    _gameUI.gameObject.SetActive(true);
                    _gameUI.SetToDefeat();
                    _menuUI.gameObject.SetActive(false);
                    _loadingUI.gameObject.SetActive(false);
                    break;

                case GameState_Type.Victory:
                    _gameUI.gameObject.SetActive(false);
                    _menuUI.gameObject.SetActive(false);
                    _loadingUI.gameObject.SetActive(false);
                    break;

                case GameState_Type.Loading:
                    _gameUI.gameObject.SetActive(false);
                    _menuUI.gameObject.SetActive(false);
                    _loadingUI.gameObject.SetActive(true);
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
