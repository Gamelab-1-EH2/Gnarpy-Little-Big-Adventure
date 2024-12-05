using GameManagement.Model;

namespace GameManagement.Behaviour
{
    public class GameState_CutScene : GameState
    {
        private string _nextScene;
        private GameState _nextState;
        public GameState_CutScene(GameManager_Model model, GameState nextState) : base(model)
        {
            _nextState = nextState;
        }
        
        public override void Enter()
        {
            base._model.GameState = GameState_Type.CutScene;
            CutScene.OnCutSceneEnded += ExitOnNextScene;
        }

        public override void Exit()
        {
            CutScene.OnCutSceneEnded -= ExitOnNextScene;
        }

        public override void Process()
        {
            
        }

        private void ExitOnNextScene() => base.OnStateExit?.Invoke(_nextState);

        public override string ToString() => "Intro";
    }
}
