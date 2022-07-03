using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Loaders.AssetLoad;
using Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Enemy
{
    public class EnemyGun : MonoBehaviour
    {
        private IAssetLoader _assetLoader;
        
        private GameObject _enemyBullet;
        private Transform _enemyBulletsParent;
        
        private bool _isShooting;

        private void Start()
        {
            _enemyBulletsParent = new GameObject("EnemyBullets").transform;

            _assetLoader = AllServices.Container.Single<IAssetLoader>();
            _enemyBullet = _assetLoader.LoadAsset(Constants.EnemyBulletName);
        }

        private void Update() => 
            WaitAndShoot();

        private async void WaitAndShoot()
        {
            if (_isShooting) return;

            _isShooting = true;
            DestroyAfterShoot();

            await UniTask.Delay(TimeSpan.FromSeconds(4));

            _isShooting = false;
        }

        private void DestroyAfterShoot()
        {
            var enemyBullet = InstantiateBullet();

            if (enemyBullet != null) enemyBullet.LifeTimeEnded += Destroy;
        }

        private EnemyBullet InstantiateBullet()
        {
            var enemyBullet =
                Instantiate(_enemyBullet, Vector3.zero, Quaternion.identity, _enemyBulletsParent)
                    .GetComponent<EnemyBullet>();

            enemyBullet.transform.position = transform.position;
            enemyBullet.Direction = transform.parent.rotation.y == 180
                ? Vector3.forward
                : Vector3.forward * -1;

            return enemyBullet;
        }
    }
}