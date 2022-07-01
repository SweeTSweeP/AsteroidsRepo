using System;
using Infrastructure.Services;
using Infrastructure.Services.Collisions;
using UnityEngine;

namespace Enemy
{
    public class EnemyBullet : MonoBehaviour
    {
        private const float Speed = 100;
        private const float MaxLifeTime = 3;

        private ICollisionDetector _collisionDetector;
        
        private float _currentLifeTime;
        
        public event Action<GameObject> LifeTimeEnded;

        public Vector3 Direction { get; set; }

        private void Start() => 
            _collisionDetector = AllServices.Container.Single<ICollisionDetector>();

        private void Update()
        {
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime >= MaxLifeTime) LifeTimeEnded?.Invoke(gameObject);

            TryToDestroy();
        }

        private void FixedUpdate() => 
            transform.position += Direction * Speed * Time.deltaTime;

        private void TryToDestroy()
        {
            var facedObject = _collisionDetector.DetectCollisionsWithSphere(transform);

            if (facedObject == null || !facedObject.CompareTag(Constants.PlayerTag)) return;
            
            Destroy(facedObject.gameObject);
            LifeTimeEnded?.Invoke(gameObject);
        }
    }
}