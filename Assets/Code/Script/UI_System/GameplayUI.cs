using UnityEngine;
using System;

namespace UI_System
{
    public class GameplayUI : MonoBehaviour
    {
        public static Action OnMainMenuRequest;
        public static Action OnReloadLevelRequest;

        private GameUI _gameUI;
        private PauseUI _pauseUI;
        private DefeatUI _defeatUI;
        private VictoryUI _victoryUI;

        private void Awake()
        {
            _gameUI = GetComponentInChildren<GameUI>(true);
            _pauseUI = GetComponentInChildren<PauseUI>(true);
            _defeatUI = GetComponentInChildren<DefeatUI>(true);
            _victoryUI = GetComponentInChildren<VictoryUI>(true);
        }

        public void SetToGame()
        {
            _gameUI.gameObject.SetActive(true);
            _pauseUI.gameObject.SetActive(false);
            _defeatUI.gameObject.SetActive(false);
            _victoryUI.gameObject.SetActive(false);
        }

        public void SetToPause()
        {
            _gameUI.gameObject.SetActive(false);
            _pauseUI.gameObject.SetActive(true);
            _defeatUI.gameObject.SetActive(false);
            _victoryUI.gameObject.SetActive(false);
        }

        public void SetToDefeat()
        {
            _gameUI.gameObject.SetActive(false);
            _pauseUI.gameObject.SetActive(false);
            _defeatUI.gameObject.SetActive(true);
            _defeatUI.Show();
            _victoryUI.gameObject.SetActive(false);
        }

        public void SetToVictory()
        {
            _gameUI.gameObject.SetActive(false);
            _pauseUI.gameObject.SetActive(false);
            _defeatUI.gameObject.SetActive(false);
            _victoryUI.gameObject.SetActive(true);
            _victoryUI.Show();
        }
    }
}
