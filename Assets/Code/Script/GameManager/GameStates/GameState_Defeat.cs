using GameManagement.Model;

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
        }

        public override void Exit()
        {
            
        }

        public override void Process()
        {
            
        }

        public override string ToString() => "Defeat";
    }
}