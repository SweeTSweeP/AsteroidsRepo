using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using Infrastructure.Services.BulletPool;
using Infrastructure.Services.ServiceLocator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Spaceship
{
    public class SpaceshipGun : MonoBehaviour
    {
        private const float BulletCooldown = 0.3f;
        private const int CountOfBullets = 7;

        private IBulletPool _bulletPool;
        private int _currentCountOfBullets = CountOfBullets;

        private bool _isShooting;

        private void Start()
        {
            _bulletPool = AllServices.Container.Single<IBulletPool>();
            _bulletPool.InitBulletsPool(transform.position);
        }

        private void Update() => 
            TryToShoot();

        private void TryToShoot()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }
            else
            {
                if (_currentCountOfBullets == 0) _isShooting = false;
            }
        }

        private async void Shoot()
        {
            if (_isShooting) return;

            _isShooting = true;
            
            var preparedBullet = _bulletPool.GetFreeBullet(transform.rotation, out _currentCountOfBullets);

            if (preparedBullet == null) return;

            preparedBullet.transform.position = transform.position;
            preparedBullet.gameObject.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(BulletCooldown));

            _isShooting = false;
        }
    }
}