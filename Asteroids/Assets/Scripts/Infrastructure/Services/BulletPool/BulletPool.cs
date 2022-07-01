using Infrastructure.Services.LevelClean;
using Infrastructure.Services.Loaders.AssetLoad;
using Infrastructure.Services.SpaceShipDataUpdate;
using Spaceship;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.BulletPool
{
    public class BulletPool : IBulletPool
    {
        private const int CountOfBullets = 7;

        private ISpaceShipDataUpdater _spaceShipDataUpdater;
        private ILevelCleaner _levelCleaner;
        
        private Bullet[] _bulletStorage;
        private Bullet[] _usedBullets;
        private Transform _parent;

        private Vector3 _startPosition;
        
        private int _countOfUsedBullets;

        public Bullet GetFreeBullet(Quaternion rotation, out int freeBulletCount)
        {
            freeBulletCount = CountOfBullets - _countOfUsedBullets;
            
            if (_countOfUsedBullets == CountOfBullets) return null;

            var bullet = _bulletStorage[_countOfUsedBullets];
            
            _bulletStorage[_countOfUsedBullets] = null;
            _usedBullets[_countOfUsedBullets] = bullet;

            bullet.transform.rotation = rotation;
            
            _countOfUsedBullets++;
            _spaceShipDataUpdater.UpdateCountOfBullets(CountOfBullets - _countOfUsedBullets);
            
            return bullet;
        }

        public void ReturnBulletToPool(Bullet bullet)
        {
             _countOfUsedBullets--;
             _spaceShipDataUpdater.UpdateCountOfBullets(CountOfBullets - _countOfUsedBullets);
            
            bullet.gameObject.SetActive(false);
            bullet.transform.position = _startPosition;
            _bulletStorage[_countOfUsedBullets] = bullet;
            _usedBullets[_countOfUsedBullets] = null;
        }

        public void InitBulletsPool(Vector3 initPosition)
        {
            var assetLoader = AllServices.Container.Single<IAssetLoader>();
            var bulletAsset = assetLoader.LoadAsset(Constants.BulletName);
            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.UpdateCountOfBullets(CountOfBullets - _countOfUsedBullets);
            _levelCleaner = AllServices.Container.Single<ILevelCleaner>();
            _startPosition = initPosition;
            _bulletStorage = new Bullet[CountOfBullets];
            _usedBullets = new Bullet[CountOfBullets];
            _parent = new GameObject("Bullets").transform;
            _levelCleaner.AddObjectToCollector(_parent.gameObject);

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