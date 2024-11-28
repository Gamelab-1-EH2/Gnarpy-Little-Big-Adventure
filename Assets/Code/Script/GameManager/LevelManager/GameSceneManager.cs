using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement.GameSceneManagement
{
    [System.Serializable]
    public class GameSceneManager
    {
        public Action<int> OnGameSceneLoaded;

        [SerializeField] private List<string> _gameScenes = new List<string>{"none"};

        public void LoadGameScene(int index)
        {
            if(_gameScenes[index] != "none")
                SceneManager.LoadScene(_gameScenes[index], LoadSceneMode.Additive);
        }
    }
}
