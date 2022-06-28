using System;
using Infrastructure.Services.Collisions;
using UnityEngine;

namespace Enemy
{
    public class PlayerDamage : MonoBehaviour
    {
        private ICollisionDetector _collisionDetector;
        
        private void Start() => 
            _collisionDetector = new CollisionDetector();

        private void Update() => 
            TryToDestroyPlayer();

        private void TryToDestroyPlayer()
        {
            var collisionObject = _collisionDetector.DetectCollisionsWithSphere(transform, 1.5f);

            if (collisionObject != null && collisionObject.CompareTag(Constants.PlayerTag)) 
                Destroy(collisionObject.transform.root.gameObject);
        }
    }
}