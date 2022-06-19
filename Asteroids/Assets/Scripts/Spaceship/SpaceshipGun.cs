using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Loaders;
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
            _bulletPool = new BulletPool(new AssetLoader());
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
            preparedBullet.InitDirection();

            await UniTask.Delay(TimeSpan.FromSeconds(BulletCooldown));

            _isShooting = false;
        }
    }
}