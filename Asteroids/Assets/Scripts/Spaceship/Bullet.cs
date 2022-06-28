using Enemy;
using Infrastructure.Services.Collisions;
using UnityEngine;

namespace Spaceship
{
    public class Bullet : MonoBehaviour
    {
        private const float Speed = 200;
        private const float MaxLifeTime = 3;

        private ICollisionDetector _collisionDetector;
        
        private Vector3 _direction;
        private float _lifeTime;

        public BulletPool BulletPool { get; set; }

        private void Start() => 
            _collisionDetector = new CollisionDetector();

        private void OnEnable() => 
            InitDirection();

        private void Update()
        {
            TryToDestroy();
            WaitForEndBulletLife();
        }

        private void FixedUpdate() => 
            transform.position += _direction * Speed * Time.deltaTime;

        private void InitDirection() => 
            _direction = transform.TransformDirection(Vector3.forward);

        private void WaitForEndBulletLife()
        {
            _lifeTime += Time.deltaTime;

            if (_lifeTime >= MaxLifeTime) ReturnToBulletPool();
        }

        private void TryToDestroy()
        {
            var detectedCollider = _collisionDetector.DetectCollisionsWithSphere(transform);

            if (detectedCollider == null || detectedCollider.gameObject == gameObject) return;
            
            var enemyDestroy = detectedCollider.gameObject.GetComponent<IDestroy>();

            enemyDestroy?.TryToDestroy();
            Destroy(detectedCollider.gameObject);
            ReturnToBulletPool();
        }

        private void ReturnToBulletPool()
        {
            _lifeTime = 0;
            BulletPool.ReturnBulletToPool(this);
        }
    }
}