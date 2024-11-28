using System;
using UnityEngine;

using GameManagement.Model;
using GameManagement.GameSceneManagement;

namespace GameManagement.Behaviour
{
    public class GameState_Loading : GameState
    {
        public static Action<float> OnSceneLoadProgressChanged;

        private GameState _nextState;
        private string _sceneName;
        private bool _unload;
        
        public GameState_Loading(GameManager_Model model, GameState nextState, string scene) : base(model)
        {
            _nextState = nextState;
            _sceneName = scene;
            _unload = false;
        }

        public GameState_Loading(GameManager_Model model, GameState nextState, string scene, bool unload) : base(model)
        {
            _nextState = nextState;
            _sceneName = scene;
            _unload = unload;
        }

        public override void Enter()
        {
            base._model.GameState = GameState_Type.Loading;
            if(!_unload)
                base._model.SceneManager.LoadGameScene(_sceneName);
            else
                base._model.SceneManager.UnloadGameScene(_sceneName);

            base._model.SceneManager.LoadOperation.completed += SceneLoaded;
            
        }

        public override void Exit()
        {
            base._model.SceneManager.LoadOperation.completed -= SceneLoaded;
        }

        public override void Process()
        {
            if(base._model.SceneManager.LoadOperation != null)
                OnSceneLoadProgressChanged?.Invoke(base._model.SceneManager.LoadOperation.progress);
        }

        private void SceneLoaded(AsyncOperation _) => base.OnStateExit?.Invoke(_nextState);

        public override string ToString() => "Loading";
    }
}
