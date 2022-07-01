using Infrastructure.Services;
using Infrastructure.Services.LevelClean;
using Infrastructure.Services.Loaders.AssetLoad;
using Infrastructure.Services.Loaders.SceneLoad;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string MainSceneName = "MainScene";

        private readonly GameStateMachine _gameStateMachine;
        private readonly Curtain _curtain;
        private readonly AllServices _services;
        private readonly SceneLoader _sceneLoader;

        private ILevelCleaner _levelCleaner;

        private GameObject spawner;
        private GameObject player;

        public LoadLevelState(
            GameStateMachine gameStateMachine, 
            Curtain curtain, AllServices services, 
            SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
            _services = services;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _levelCleaner = _services.Single<ILevelCleaner>();
            _sceneLoader.Load(MainSceneName, EnterLoadLevel);
        }

        public void Exit() => 
            _curtain.HideCurtain();

        private void LoadAssets()
        {
            var assetLoader = _services.Single<IAssetLoader>();
            
            spawner = assetLoader.LoadAsset(Constants.SpawnerName);
            player = assetLoader.LoadAsset(Constants.PlayerName);
        }

        private void EnterLoadLevel()
        {
            LoadAssets();
            InitPlayer();

            _levelCleaner.AddObjectToCollector(InitSpawner());
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitPlayer() =>
            Object.Instantiate(player, Vector3.zero, Quaternion.identity);

        private GameObject InitSpawner() => 
            Object.Instantiate(spawner, Vector3.zero, Quaternion.identity);
    }
}