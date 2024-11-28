using GameManagement.Model;
using StateMachines.States;

namespace GameManagement.Behaviour
{
    public abstract class GameState : State
    {
        protected GameManager_Model _model;
        public GameState(GameManager_Model model)
        {
            _model = model;
        }
    }
}
