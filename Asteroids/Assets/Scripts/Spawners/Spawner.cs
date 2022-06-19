using Asteroid;
using Enemy;
using Infrastructure.Loaders;
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

        private int _сountOfAsteroids = 5;
        private int _countOfEnemy = 0;
        private int _currentAsteroidsCount;
        private int _currentEnemiesCount;

        private void Start()
        {
            _assetLoader = new AssetLoader();
            
            _asteroid = _assetLoader.LoadAsset(Constants.AsteroidName);
            _asteroidChild = _assetLoader.LoadAsset(Constants.AsteroidChildName);
            _enemy = _assetLoader.LoadAsset(Constants.EnemyName);
            
            SpawnAsteroids();
        }

        private void SpawnAsteroids()
        {
            for (var i = 0; i < _сountOfAsteroids; i++)
            {
                var asteroid = Instantiate(_asteroid, Vector3.zero, Quaternion.identity, transform);
                var enemyDestroy = asteroid.GetComponent<AsteroidDestroy>();

                if (enemyDestroy != null) enemyDestroy.ObjectDestroyed += OnAsteroidDestroyed;

                _currentAsteroidsCount++;
            }
        }

        private void SpawnEnemies()
        {
        }

        private void OnAsteroidDestroyed(Vector3 position, AsteroidType asteroidType)
        {
            if (asteroidType == AsteroidType.Parent)
            {
                SpawnAsteroidChild();
                SpawnAsteroidChild();
            }

            _currentAsteroidsCount--;
            
            void SpawnAsteroidChild()
            {
                Instantiate(_asteroidChild, position, Quaternion.identity, transform);
                _currentAsteroidsCount++;
            }

            if (_currentAsteroidsCount == 0 && _currentEnemiesCount == 0)
            {
                SpawnAsteroids();
                SpawnEnemies();
            }
        }
    }
}