using Infrastructure.Loaders;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Spaceship
{
    public class BulletPool
    {
        private const int CountOfBullets = 15;

        private IAssetLoader _assetLoader;
        
        private Bullet[] _bulletStorage;
        private Bullet[] _usedBullets;
        private Transform _parent;

        private Vector3 _startPosition;
        
        private int _countOfUsedBullets;

        public BulletPool(IAssetLoader assetLoader) => 
            _assetLoader = assetLoader;

        public Bullet GetFreeBullet(Quaternion rotation)
        {
            if (_countOfUsedBullets == CountOfBullets) return null;

            var bullet = _bulletStorage[_countOfUsedBullets];
            
            _bulletStorage[_countOfUsedBullets] = null;
            _usedBullets[_countOfUsedBullets] = bullet;

            bullet.transform.rotation = rotation;
            
            _countOfUsedBullets++;
            
            return bullet;
        }

        public void ReturnBulletToPool(Bullet bullet)
        {
             _countOfUsedBullets--;
            
            bullet.gameObject.SetActive(false);
            bullet.transform.position = _startPosition;
            _bulletStorage[_countOfUsedBullets] = bullet;
            _usedBullets[_countOfUsedBullets] = null;
        }

        public void InitBulletsPool(Vector3 initPosition)
        {
            var bulletAsset = _assetLoader.LoadAsset(Constants.BulletName);
            _startPosition = initPosition;
            _bulletStorage = new Bullet[CountOfBullets];
            _usedBullets = new Bullet[CountOfBullets];
            _parent = new GameObject("Bullets").transform;

            for (var i = 0; i < CountOfBullets; i++)
            {
                _bulletStorage[i] = Object
                    .Instantiate(bulletAsset, _startPosition, Quaternion.identity, _parent)
                    .GetComponent<Bullet>();

                var bullet = _bulletStorage[i].GetComponent<Bullet>();

                if (bullet != null) bullet.BulletPool = this;

                _bulletStorage[i].gameObject.SetActive(false);
            }
        }
    }
}