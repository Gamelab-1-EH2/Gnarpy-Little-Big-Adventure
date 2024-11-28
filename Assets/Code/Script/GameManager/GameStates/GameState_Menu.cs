using GameManagement.Model;

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
        }

        public override void Exit()
        {
            
        }

        public override void Process()
        {
            
        }

        public override string ToString() => "Pause";
    }
}
