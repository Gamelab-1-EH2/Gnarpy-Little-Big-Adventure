using UnityEngine;

namespace UI_System
{
    public class GameplayUI : MonoBehaviour
    {
        private GameUI _gameUI;
        private PauseUI _pauseUI;

        private void Awake()
        {
            _gameUI = GetComponentInChildren<GameUI>(true);
            _pauseUI = GetComponentInChildren<PauseUI>(true);
        }

        public void SetToGame()
        {
            _gameUI.gameObject.SetActive(true);
            _pauseUI.gameObject.SetActive(false);
        }

        public void SetToPause()
        {
            _gameUI.gameObject.SetActive(false);
            _pauseUI.gameObject.SetActive(true);
        }
    }
}
