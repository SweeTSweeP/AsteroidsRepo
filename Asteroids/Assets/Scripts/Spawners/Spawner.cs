using Asteroid;
using Enemy;
using Infrastructure.Services;
using Infrastructure.Services.Loaders;
using Infrastructure.Services.Loaders.AssetLoad;
using UnityEngine;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        private const int MaxCountOfAsteroids = 8;
        private const int MaxCountOfEnemies = 2;
        
        private IAssetLoader _assetLoader;
        
        private GameObject _asteroid;
        private GameObject _asteroidChild;
        private GameObject _enemy;

        private int _countOfAsteroids = 5;
        private int _countOfEnemy = 0;
        private int _currentAsteroidsCount;
        private int _currentEnemiesCount;

        private void Start()
        {
            _assetLoader = AllServices.Container.Single<IAssetLoader>();
            _asteroid = _assetLoader.LoadAsset(Constants.AsteroidName);
            _asteroidChild = _assetLoader.LoadAsset(Constants.AsteroidChildName);
            _enemy = _assetLoader.LoadAsset(Constants.EnemyName);
            
            SpawnAsteroids();
        }

        private void SpawnAsteroids()
        {
            for (var i = 0; i < _countOfAsteroids; i++) 
                InstantiateAsteroid(_asteroid);
        }

        private void SpawnEnemies()
        {
            for (var i = 0; i < _countOfEnemy; i++)
            {
                var enemy = Instantiate(_enemy, Vector3.zero, Quaternion.identity, transform);
                var enemyDestroy = enemy.GetComponent<EnemyDestroy>();

                if (enemyDestroy != null) enemyDestroy.ObjectDestroyed += OnEnemyDestroyed;

                _currentEnemiesCount++;
            }
        }

        private void OnEnemyDestroyed(Vector3 position)
        {
            _currentEnemiesCount--;
            
            NextStage();
        }

        private void OnAsteroidDestroyed(Vector3 position, AsteroidType asteroidType)
        {
            if (asteroidType == AsteroidType.Parent)
            {
                InstantiateAsteroid(_asteroidChild, position);
                InstantiateAsteroid(_asteroidChild, position);
            }

            _currentAsteroidsCount--;
            
            NextStage();
        }

        private void InstantiateAsteroid(GameObject asteroidAsset, Vector3 position = default)
        {
            var asteroid = Instantiate(asteroidAsset, position, Quaternion.identity, transform);
            var asteroidDestroy = asteroid.GetComponent<AsteroidDestroy>();

            if (asteroidDestroy != null) asteroidDestroy.ObjectDestroyed += OnAsteroidDestroyed;

            _currentAsteroidsCount++;
        }

        private void NextStage()
        {
            if (_currentAsteroidsCount != 0 || _currentEnemiesCount != 0) return;
            if (_countOfEnemy < MaxCountOfEnemies) _countOfEnemy++;
            if (_countOfAsteroids < MaxCountOfAsteroids) _countOfAsteroids++;

            SpawnAsteroids();
            SpawnEnemies();
        }
    }
}