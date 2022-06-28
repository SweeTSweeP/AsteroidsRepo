using UnityEngine;

namespace Spaceship
{
    public interface IBulletPool
    {
        Bullet GetFreeBullet(Quaternion rotation);
        void ReturnBulletToPool(Bullet bullet);
        void InitBulletsPool(Vector3 initPosition);
    }
}