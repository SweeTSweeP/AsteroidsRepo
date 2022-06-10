using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Spaceship
{
    public class SpaceshipGun : MonoBehaviour
    {
        private const int CountOfBullets = 15;
        private const float BulletCooldown = 0.3f;

        private GameObject[] _bulletStorage;
        private GameObject[] _usedBullets;
        private Transform _parent;

        private int _countOfUsedBullets;
        private bool _isShooting;

        private void Start() => 
            InitBulletsPool();

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) Shoot();
        }

        private async void Shoot()
        {
            if (_isShooting) return;

            _isShooting = true;
            
            var preparedBullet = GetFreeBullet();

            if (preparedBullet == null) return;

            preparedBullet.transform.position = transform.position;
            preparedBullet.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(BulletCooldown));

            _isShooting = false;
        }

        private GameObject GetFreeBullet()
        {
            if (_countOfUsedBullets == CountOfBullets) return null;

            var bullet = _bulletStorage[_countOfUsedBullets];
            
            _bulletStorage[_countOfUsedBullets] = null;
            _usedBullets[_countOfUsedBullets] = bullet;

            bullet.transform.rotation = transform.rotation;
            
            _countOfUsedBullets++;
            
            ReturnBulletToPool(bullet);

            return bullet;
        }

        private async void ReturnBulletToPool(GameObject bullet)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            
            _countOfUsedBullets--;
            
            bullet.SetActive(false);
            bullet.transform.position = transform.position;

            _bulletStorage[_countOfUsedBullets] = bullet;
            _usedBullets[_countOfUsedBullets] = null;
        }


        private void InitBulletsPool()
        {
            var bulletAsset = LoadBullet();
            _bulletStorage = new GameObject[CountOfBullets];
            _usedBullets = new GameObject[CountOfBullets];
            _parent = new GameObject("Bullets").transform;

            for (var i = 0; i < CountOfBullets; i++)
            {
                _bulletStorage[i] = Instantiate(bulletAsset, transform.position, Quaternion.identity, _parent);
                _bulletStorage[i].SetActive(false);
            }
        }

        private GameObject LoadBullet() => 
            Addressables.LoadAssetAsync<GameObject>(Constants.BulletName).WaitForCompletion();
    }
}