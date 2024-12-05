using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement.GameSceneManagement
{
    [System.Serializable]
    public class GameSceneManager
    {
        [SerializeField] private List<string> _gameScenes = new List<string>{"none"};
        [SerializeField] private string _intro = "none";
        [SerializeField] private string _winOutro = "none";

        private AsyncOperation _loadOperation;
        
        public GameSceneManager()
        {
            _loadOperation = new AsyncOperation();
        }

        public void LoadGameScene(int index)
        {
            if(_gameScenes[index] != "none")
                _loadOperation = SceneManager.LoadSceneAsync(_gameScenes[index], LoadSceneMode.Additive);
        }

        public void LoadGameScene(string sceneToLoad)
        {
            if (sceneToLoad != "none")
                _loadOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        }

        public void UnloadGameScene(int index)
        {
            if (_gameScenes[index] != "none")
                _loadOperation = SceneManager.UnloadSceneAsync(_gameScenes[index]);
        }

        public void UnloadGameScene(string scene)
        {
            if (scene != "none")
                _loadOperation = SceneManager.UnloadSceneAsync(scene);
        }

        public string IntroScene => _intro;
        public string WinScene => _winOutro;
        public AsyncOperation LoadOperation => _loadOperation;
        public List<String> GameScenes => _gameScenes;
    }
}
