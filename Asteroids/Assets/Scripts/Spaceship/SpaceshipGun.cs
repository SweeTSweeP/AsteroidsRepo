using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.Services.Loaders;
using Infrastructure.Services.Loaders.AssetLoad;
using UnityEngine;

namespace Spaceship
{
    public class SpaceshipGun : MonoBehaviour
    {
        private const float BulletCooldown = 0.3f;

        private BulletPool _bulletPool;

        private bool _isShooting;

        private void Start()
        {
            _bulletPool = new BulletPool(AllServices.Container.Single<IAssetLoader>());
            _bulletPool.InitBulletsPool(transform.position);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) Shoot();
        }

        private async void Shoot()
        {
            if (_isShooting) return;

            _isShooting = true;
            
            var preparedBullet = _bulletPool.GetFreeBullet(transform.rotation);

            if (preparedBullet == null) return;

            preparedBullet.transform.position = transform.position;
            preparedBullet.gameObject.SetActive(true);
            //preparedBullet.InitDirection();

            await UniTask.Delay(TimeSpan.FromSeconds(BulletCooldown));

            _isShooting = false;
        }
    }
}