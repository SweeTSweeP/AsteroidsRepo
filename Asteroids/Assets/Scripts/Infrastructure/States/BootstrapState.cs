using Infrastructure.Services;
using Infrastructure.Services.Collisions;
using Infrastructure.Services.Loaders.AssetLoad;
using Infrastructure.Services.Loaders.SceneLoad;
using UI;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string MainSceneName = "MainScene";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly Curtain _curtain;

        public BootstrapState(
            GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            AllServices services,
            Curtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _curtain = curtain;
        }

        public void Enter()
        {
            RegisterServices();
            _curtain.ShowCurtain();
            _sceneLoader.Load(MainSceneName, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetLoader>(new AssetLoader());
            _services.RegisterSingle<ICollisionDetector>(new CollisionDetector());
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState>();
    }
}