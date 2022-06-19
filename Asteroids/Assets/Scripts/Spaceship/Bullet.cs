using Asteroid;
using Enemy;
using UnityEngine;

namespace Spaceship
{
    public class Bullet : MonoBehaviour
    {
        private const float Speed = 200;
        private const float MaxLifeTime = 3;
        
        private Vector3 _direction;
        private float _lifeTime;

        public BulletPool BulletPool { get; set; }

        private void Update()
        {
            TryToDestroy();
            WaitForEndBulletLife();
        }

        private void FixedUpdate() => 
            transform.position += _direction * Speed * Time.deltaTime;

        public void InitDirection() => 
            _direction = transform.TransformDirection(Vector3.forward);

        private void WaitForEndBulletLife()
        {
            _lifeTime += Time.deltaTime;

            if (_lifeTime >= MaxLifeTime) ReturnToBulletPool();
        }

        private void TryToDestroy()
        {
            var detectedCollider = DetectCollisions();

            if (detectedCollider == null || detectedCollider.gameObject == gameObject) return;
            
            var enemyDestroy = detectedCollider.gameObject.GetComponent<AsteroidDestroy>();
            
            if (enemyDestroy != null) enemyDestroy.TryToDestroy();
            
            Destroy(detectedCollider.gameObject);
            
            ReturnToBulletPool();
        }

        private Collider DetectCollisions()
        {
            var hitColliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(transform.position, 0.1f, hitColliders);

            return hitColliders[0] != null ? hitColliders[0] : null;
        }

        private void ReturnToBulletPool()
        {
            _lifeTime = 0;
            BulletPool.ReturnBulletToPool(this);
        }
    }
}