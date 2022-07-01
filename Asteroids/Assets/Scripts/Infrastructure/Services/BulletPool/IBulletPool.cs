using Infrastructure.Services.Loaders.AssetLoad;
using Spaceship;
using UnityEngine;

namespace Infrastructure.Services.BulletPool
{
    public interface IBulletPool : IService
    {
        Bullet GetFreeBullet(Quaternion rotation, out int freeBulletCount);
        void ReturnBulletToPool(Bullet bullet);
        void InitBulletsPool(Vector3 initPosition);
    }
}