using UnityEngine.InputSystem;

using GameManagement.Model;

namespace GameManagement.Behaviour
{
    public class GameState_Pause : GameState
    {
        public GameState_Pause(GameManager_Model model) : base(model)
        {

        }

        public override void Enter()
        {
            base._model.GameState = GameState_Type.Pause;
            InputManager.ActionMap.Pause.TogglePause.started += ResumeGame;
        }

        public override void Exit()
        {
            InputManager.ActionMap.Pause.TogglePause.started -= ResumeGame;
        }

        public override void Process()
        {
            
        }

        private void ResumeGame(InputAction.CallbackContext _) => base.OnStateExit(new GameState_Gameplay(base._model));


        public override string ToString() => "Pause";
    }
}
