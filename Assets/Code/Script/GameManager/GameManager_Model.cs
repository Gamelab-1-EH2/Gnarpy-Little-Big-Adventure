using Turret_System;
using System;

namespace GameManagement.Model
{
    public class GameManager_Model
    {
        public Action<GameState_Type> OnStateChanged;

        private DroppableManager _droppableManager;
        private TurretManager _turretManager;
        private FallableManager _fallableManager;

        private GameState_Type _gameState;
        public GameManager_Model(DroppableManager droppableManager, TurretManager turretManager)
        {
            _droppableManager = droppableManager;
            _turretManager = turretManager;
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
