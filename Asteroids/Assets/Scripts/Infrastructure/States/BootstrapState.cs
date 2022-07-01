using Infrastructure.Services;
using Infrastructure.Services.AsteroidPositioner;
using Infrastructure.Services.BulletPool;
using Infrastructure.Services.Collisions;
using Infrastructure.Services.EnemyPositioner;
using Infrastructure.Services.LevelClean;
using Infrastructure.Services.Loaders.AssetLoad;
using Infrastructure.Services.Player;
using Infrastructure.Services.RestartButton;
using Infrastructure.Services.Score;
using Infrastructure.Services.SpaceShipDataUpdate;
using UI;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly AllServices _services;
        private readonly Curtain _curtain;

        public BootstrapState(
            GameStateMachine gameStateMachine, 
            AllServices services,
            Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _services = services;
            _curtain = curtain;
        }

        public void Enter()
        {
            RegisterServices();
            _curtain.ShowCurtain();
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetLoader>(new AssetLoader());
            _services.RegisterSingle<ICollisionDetector>(new CollisionDetector());
            _services.RegisterSingle<IBulletPool>(new BulletPool());
            _services.RegisterSingle<IAsteroidPositioner>(new AsteroidPositioner());
            _services.RegisterSingle<IEnemyPositioner>(new EnemyPositioner());
            _services.RegisterSingle<ISpaceShipDataUpdater>(new SpaceShipDataUpdater());
            _services.RegisterSingle<IPlayerDeathIndicator>(new PlayerDeathIndicator());
            _services.RegisterSingle<IRestartButtonTrigger>(new RestartButtonTrigger());
            _services.RegisterSingle<ILevelCleaner>(new LevelCleaner());
            _services.RegisterSingle<IScoreUpdater>(new ScoreUpdater());
        }
    }
}