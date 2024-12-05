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
            string cutScene = _model.SceneManager.IntroScene;
            string gameScene = _model.SceneManager.GameScenes[0];
            
            GameState loadingGameScene = new GameState_Loading(base._model, new GameState_Gameplay(base._model), gameScene);
            GameState unloadingCutScene = new GameState_Loading(base._model, loadingGameScene, cutScene, true);
            GameState loadingCutScene = new GameState_Loading(base._model, new GameState_CutScene(base._model, unloadingCutScene), cutScene);

            base.OnStateExit?.Invoke(loadingCutScene);
        }

        public override void Process()
        {
            
        }

        public override string ToString() => "Pause";
    }
}
