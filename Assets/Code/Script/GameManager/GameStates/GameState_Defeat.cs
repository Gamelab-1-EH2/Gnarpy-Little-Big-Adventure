using GameManagement.Model;
using UI_System;

namespace GameManagement.Behaviour
{
    public class GameState_Defeat : GameState
    {
        public GameState_Defeat(GameManager_Model model) : base(model)
        {

        }

        public override void Enter()
        {
            base._model.GameState = GameState_Type.Defeat;
            GameplayUI.OnMainMenuRequest += QuitToMenu;
            GameplayUI.OnReloadLevelRequest += ReloadLevel;
        }

        public override void Exit()
        {
            GameplayUI.OnMainMenuRequest -= QuitToMenu;
            GameplayUI.OnReloadLevelRequest -= ReloadLevel;
        }

        public override void Process()
        {
            
        }

        public void ReloadLevel()
        {
            base._model.SceneManager.UnloadGameScene(base._model.SceneManager.GameScenes[0]);
            base.OnStateExit(new GameState_Loading(base._model, new GameState_Gameplay(base._model), base._model.SceneManager.GameScenes[0]));
        }

        public void QuitToMenu()
        {
            base.OnStateExit(new GameState_Loading(base._model, new GameState_Menu(base._model), base._model.SceneManager.GameScenes[0], true));
        }

        public override string ToString() => "Defeat";
    }
}