using System;
using Enemy;
using Infrastructure.Services;
using Infrastructure.Services.BulletPool;
using Infrastructure.Services.Collisions;
using Infrastructure.Services.Score;
using UnityEngine;

namespace Spaceship
{
    public class Bullet : MonoBehaviour
    {
        private const float Speed = 200;
        private const float MaxLifeTime = 3;

        private ICollisionDetector _collisionDetector;
        private IScoreUpdater _scoreUpdater;
        
        private Vector3 _direction;
        private float _lifeTime;

        public BulletPool BulletPool { get; set; }

        private void Start() => 
            InitServices();

        private void OnEnable() => 
            InitDirection();

        private void Update()
        {
            TryToDestroy();
            WaitForEndBulletLife();
        }

        private void FixedUpdate() => 
            transform.position += _direction * Speed * Time.deltaTime;

        private void InitServices()
        {
            _collisionDetector = AllServices.Container.Single<ICollisionDetector>();
            _scoreUpdater = AllServices.Container.Single<IScoreUpdater>();
        }

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
            NotifyScore(detectedCollider);
            Destroy(detectedCollider.gameObject);
            ReturnToBulletPool();
        }

        private void NotifyScore(Collider detectedCollider)
        {
            Enum.TryParse(detectedCollider.tag, out EnemyTag enemyTag);
            _scoreUpdater.UpdateScore(enemyTag);
        }

        private void ReturnToBulletPool()
        {
            _lifeTime = 0;
            BulletPool.ReturnBulletToPool(this);
        }
    }
}