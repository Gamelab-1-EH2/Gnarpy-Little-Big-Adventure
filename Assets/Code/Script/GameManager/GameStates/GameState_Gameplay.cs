using UnityEngine;
using UnityEngine.InputSystem;

using Player;
using GameManagement.Model;

namespace GameManagement.Behaviour
{
    public class GameState_Gameplay : GameState
    {
        public GameState_Gameplay(GameManager_Model model) : base(model)
        {

        }

        public override void Enter()
        {
            if(base._model.GameState != GameState_Type.Pause)
            {
                _model.DroppableManager.Start();
                _model.FallableManager.Start();
                _model.TurretManager.Start(base._model.ManagerTransform);
            }
            
            MonoBehaviour.FindObjectOfType<PlayerController>().OnPlayerDeath += DefeatExit;
            MonoBehaviour.FindObjectOfType<BossController>(true).OnBossDefeat += VictoryExit;

            InputManager.ActionMap.Pause.TogglePause.started += PauseGame;
            Time.timeScale = 1f;

            base._model.GameState = GameState_Type.Gameplay;
        }

        public override void Exit()
        {
            MonoBehaviour.FindObjectOfType<PlayerController>().OnPlayerDeath -= DefeatExit;
            MonoBehaviour.FindObjectOfType<BossController>(true).OnBossDefeat -= VictoryExit;

            InputManager.ActionMap.Pause.TogglePause.started -= PauseGame;
        }

        public override void Process()
        {
            _model.FallableManager.Process();
            _model.TurretManager.Process();
        }

        private void DefeatExit() => base.OnStateExit(new GameState_Defeat(base._model));
        private void VictoryExit()
        {
            string cutScene = _model.SceneManager.WinScene;
            string gameScene = _model.SceneManager.GameScenes[0];
            
            GameState loadMenu = new GameState_Menu(base._model);
            GameState loadingCutScene = new GameState_Loading(base._model, new GameState_CutScene(base._model, loadMenu), cutScene);
            GameState unloadingGameScene = new GameState_Loading(base._model, loadingCutScene, gameScene, true);

            base.OnStateExit?.Invoke(unloadingGameScene);
        }

        private void PauseGame(InputAction.CallbackContext _) => base.OnStateExit(new GameState_Pause(base._model));

        public override string ToString() => "Gameplay";
    }
}
