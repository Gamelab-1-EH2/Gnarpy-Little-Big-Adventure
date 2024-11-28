using UnityEngine;
using UnityEngine.InputSystem;

using GameManagement.Model;

namespace GameManagement.Behaviour
{
    public class GameState_Pause : GameState
    {

        bool _canExit;

        public GameState_Pause(GameManager_Model model) : base(model)
        {

        }

        public override void Enter()
        {
            InputManager.ActionMap.Pause.TogglePause.performed += ResumeGame;
            base._model.GameState = GameState_Type.Pause;
            _canExit = false;
            Time.timeScale = 0f;
        }

        public override void Exit()
        {
            InputManager.ActionMap.Pause.TogglePause.performed -= ResumeGame;
        }

        public override void Process()
        {
            
        }

        private void ResumeGame(InputAction.CallbackContext _)
        {
            if(_canExit)
            {
                base.OnStateExit(new GameState_Gameplay(base._model));
                Time.timeScale = 1f;
            }
            _canExit = true;
        }


        public override string ToString() => "Pause";
    }
}
