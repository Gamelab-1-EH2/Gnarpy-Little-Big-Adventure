using Turret_System;
using System;
using UnityEngine;

using GameManagement.GameSceneManagement;

namespace GameManagement.Model
{
    public class GameManager_Model
    {
        public Action<GameState_Type> OnStateChanged;

        private Transform _gmTransform;
        private DroppableManager _droppableManager;
        private TurretManager _turretManager;
        private FallableManager _fallableManager;
        private GameSceneManager _sceneManager;

        private GameState_Type _gameState;
        public GameManager_Model(DroppableManager droppableManager, TurretManager turretManager, GameSceneManager sceneManager)
        {
            _droppableManager = droppableManager;
            _turretManager = turretManager;
            _sceneManager = sceneManager;
            _fallableManager = new FallableManager();
        }

        public DroppableManager DroppableManager
        {
            get => _droppableManager;
            set => _droppableManager = value;
        }

        public FallableManager FallableManager
        {
            get => _fallableManager;
            set => _fallableManager = value;
        }

        public TurretManager TurretManager
        {
            get => _turretManager;
            set => _turretManager = value;
        }

        public GameSceneManager SceneManager
        {
            get => _sceneManager;
            set => _sceneManager = value;
        }

        public Transform ManagerTransform => _gmTransform;
        public void SetManagerTransform(Transform tr) => _gmTransform = tr;

        public GameState_Type GameState
        {
            get => _gameState; 
            set
            {
                _gameState = value;
                OnStateChanged?.Invoke(_gameState);
            }
        }
    }
}
