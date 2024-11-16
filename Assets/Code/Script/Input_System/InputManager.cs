using GameManagement;

public static class InputManager
{
    public static PlayerInput ActionMap;

    static InputManager()
    {
        ActionMap = new PlayerInput();
        GameManager.OnGameStateChange += ChangeInputScheme;
    }
    
    private static void ChangeInputScheme(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Gameplay:
                ActionMap.Disable();
                ActionMap.Gameplay.Enable();
                break;

            case GameState.Menu:
                ActionMap.Disable();
                ActionMap.Menu.Enable();
                break;

            case GameState.Pause:
                ActionMap.Disable();
                ActionMap.Pause.Enable();
                break;
        }
    }
}