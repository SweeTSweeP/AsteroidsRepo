using Infrastructure.Services;
using Infrastructure.Services.LevelClean;
using Infrastructure.Services.RestartButton;
using Infrastructure.Services.ServiceLocator;
using UI.Curtain;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;
        private readonly Curtain _curtain;

        private IRestartButtonTrigger _restartButtonTrigger;
        private ILevelCleaner _levelCleaner;

        public GameLoopState(GameStateMachine gameStateMachine, AllServices services, Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _services = services;
            _curtain = curtain;
        }

        public void Enter() => 
            InitServices();

        public void Exit()
        {
            _curtain.ShowCurtain();
            _levelCleaner.CleanCollector();
        }

        private void InitServices()
        {
            _levelCleaner = _services.Single<ILevelCleaner>();
            _restartButtonTrigger = _services.Single<IRestartButtonTrigger>();
            _restartButtonTrigger.RestartButtonClicked += ReloadState;
        }

        private void ReloadState() => 
            _gameStateMachine.Enter<LoadLevelState>();
    }
}