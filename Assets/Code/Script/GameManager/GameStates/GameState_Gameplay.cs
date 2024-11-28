using UnityEngine;
using UnityEngine.InputSystem;

using Player;
using GameManagement.Model;

namespace GameManagement.Behaviour
{
    public class GameState_Gameplay : GameState
    {
        public GameState_Gameplay(GameManager_Model model) : base(model)
        {

        }

        public override void Enter()
        {
            if (base._model.GameState == GameState_Type.Menu)
            {
                _model.DroppableManager.Start();
                _model.FallableManager.Start();
                _model.TurretManager.Start(base._model.ManagerTransform);
            }

            MonoBehaviour.FindObjectOfType<PlayerController>().OnPlayerDeath += DefeatExit;
            InputManager.ActionMap.Pause.TogglePause.started += PauseGame;

            base._model.GameState = GameState_Type.Gameplay;
        }

        public override void Exit()
        {
            MonoBehaviour.FindObjectOfType<PlayerController>().OnPlayerDeath -= DefeatExit;
            InputManager.ActionMap.Pause.TogglePause.started -= PauseGame;
        }

        public override void Process()
        {
            _model.FallableManager.Process();
            _model.TurretManager.Process();
        }

        private void DefeatExit() => base.OnStateExit(new GameState_Defeat(base._model));

        private void PauseGame(InputAction.CallbackContext _) => base.OnStateExit(new GameState_Pause(base._model));

        public override string ToString() => "Gameplay";
    }
}
