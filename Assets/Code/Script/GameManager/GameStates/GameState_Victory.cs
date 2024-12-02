using GameManagement.Behaviour;
using GameManagement.Model;
using UI_System;

public class GameState_Victory : GameState
{
    public GameState_Victory(GameManager_Model model) : base(model)
    {
        
    }

    public override void Enter()
    {
        GameplayUI.OnMainMenuRequest += QuitToMenu;
    }

    public override void Exit()
    {
        GameplayUI.OnMainMenuRequest -= QuitToMenu;
    }

    public override void Process()
    {
        
    }

    public void QuitToMenu() => base.OnStateExit(new GameState_Loading(base._model, new GameState_Menu(base._model), base._model.SceneManager.GameScenes[0], true));

    public override string ToString() => "Victory";
}
