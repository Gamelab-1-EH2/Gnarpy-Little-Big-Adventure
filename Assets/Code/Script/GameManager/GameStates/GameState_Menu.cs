using GameManagement.Model;
using UnityEngine;

namespace GameManagement.Behaviour
{
    public class GameState_Menu : GameState
    {
        public GameState_Menu(GameManager_Model model) : base(model)
        {

        }

        public override void Enter()
        {
            base._model.GameState = GameState_Type.Menu;
            MenuUI.OnNewGameRequest += StartNewGame;
        }

        public override void Exit()
        {
            MenuUI.OnNewGameRequest -= StartNewGame;
        }

        public void StartNewGame()
        {
            string gameScene = _model.SceneManager.GameScenes[0];
            base.OnStateExit?.Invoke(new GameState_Loading(base._model, new GameState_Gameplay(base._model), gameScene));
        }

        public override void Process()
        {
            
        }

        public override string ToString() => "Pause";
    }
}
