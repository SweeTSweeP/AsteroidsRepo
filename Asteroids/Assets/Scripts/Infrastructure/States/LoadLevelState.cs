using Infrastructure.Services;
using Infrastructure.Services.Loaders.AssetLoad;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly Curtain _curtain;
        private readonly AllServices _services;
        
        private GameObject spawner;
        private GameObject player;

        public LoadLevelState(GameStateMachine gameStateMachine, Curtain curtain, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
            _services = services;
        }

        public void Enter()
        {
            LoadAssets();
            
            InitPlayer();
            InitSpawner();

            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit() => 
            _curtain.HideCurtain();

        private void LoadAssets()
        {
            var assetLoader = _services.Single<IAssetLoader>();
            
            spawner = assetLoader.LoadAsset(Constants.SpawnerName);
            player = assetLoader.LoadAsset(Constants.PlayerName);
        }

        private void InitPlayer() =>
            Object.Instantiate(player, Vector3.zero, Quaternion.identity);

        private void InitSpawner() => 
            Object.Instantiate(spawner, Vector3.zero, Quaternion.identity);
    }
}