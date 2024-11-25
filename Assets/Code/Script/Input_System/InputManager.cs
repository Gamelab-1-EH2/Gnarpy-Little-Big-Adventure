using GameManagement;

public static class InputManager
{
    public static PlayerInput ActionMap;

    static InputManager()
    {
        ActionMap = new PlayerInput();
        GameManager.OnGameStateChange += ChangeInputScheme;
    }
    
    private static void ChangeInputScheme(GameState_Type gameState)
    {
        switch(gameState)
        {
            case GameState_Type.Gameplay:
                ActionMap.Disable();
                ActionMap.Gameplay.Enable();
                ActionMap.Pause.TogglePause.Enable();
                break;

            case GameState_Type.Menu:
                ActionMap.Disable();
                ActionMap.Menu.Enable();
                break;

            case GameState_Type.Pause:
                ActionMap.Disable();
                ActionMap.Pause.Enable();
                break;

            case GameState_Type.Victory:
                break;

            case GameState_Type.Defeat:
                break;
        }
    }
}