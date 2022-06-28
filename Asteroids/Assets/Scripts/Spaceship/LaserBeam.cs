using System;
using Cysharp.Threading.Tasks;
using Enemy;
using Infrastructure.Services.Collisions;
using UnityEngine;

namespace Spaceship
{
    public class LaserBeam : MonoBehaviour
    {
        private const float LaserCoolDown = 2;
        private const float LaserAttackTime = 0.75f;
        
        [SerializeField] private GameObject laserBeam;
        [SerializeField] private Transform tail;
        [SerializeField] private Transform head;

        private ICollisionDetector _collisionDetector;

        private float _currentLaserCooldown;
        private bool _isGunPrepared;
        private bool _isShooting;

        private void Start() => 
            _collisionDetector = new CollisionDetector();

        private void Update()
        {
            PrepareGun();
            TryToShoot();
        }

        private void PrepareGun()
        {
            if (_isGunPrepared) return;

            if (!_isGunPrepared && _currentLaserCooldown < LaserCoolDown)
                _currentLaserCooldown += Time.deltaTime;
            else
                _isGunPrepared = true;
        }

        private void TryToShoot()
        {
            if (! _isShooting && _isGunPrepared && Input.GetKey(KeyCode.E)) Shoot();
            
            if (_isShooting) DestroyReachedObjects();
        }

        private async void Shoot()
        {
            _isShooting = true;
            laserBeam.SetActive(true);

            await UniTask.Delay(TimeSpan.FromSeconds(LaserAttackTime));
            
            laserBeam.SetActive(false);
            _isShooting = false;
            _isGunPrepared = false;
            _currentLaserCooldown = 0;
        }

        private void DestroyReachedObjects()
        {
            var collisionObjects = _collisionDetector.DetectCollisionsWithCapsule(tail.position, head.position);

            foreach (var reachedCollider in collisionObjects)
            {
                var destroyObject = reachedCollider.GetComponent<IDestroy>();

                if (destroyObject == null) continue;
                
                destroyObject.TryToDestroy();
                Destroy(reachedCollider.gameObject);
            }
        }
    }
}
