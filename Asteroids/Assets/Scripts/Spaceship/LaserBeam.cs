using System;
using Cysharp.Threading.Tasks;
using Enemy;
using Infrastructure.Services;
using Infrastructure.Services.Collisions;
using Infrastructure.Services.Score;
using Infrastructure.Services.SpaceShipDataUpdate;
using UnityEngine;

namespace Spaceship
{
    public class LaserBeam : MonoBehaviour
    {
        private const float LaserCoolDown = 6;
        private const float LaserAttackTime = 0.75f;
        private const int MaxChargeCount = 3;
        
        [SerializeField] private GameObject laserBeam;
        [SerializeField] private Transform tail;
        [SerializeField] private Transform head;

        private ICollisionDetector _collisionDetector;
        private ISpaceShipDataUpdater _spaceShipDataUpdater;
        private IScoreUpdater _scoreUpdater;

        private float _currentLaserCooldown;
        private bool _isShooting;
        private int _chargeCount;

        private void Start() => 
            InitServices();

        private void Update()
        {
            PrepareGun();
            TryToShoot();
        }

        private void InitServices()
        {
            _scoreUpdater = AllServices.Container.Single<IScoreUpdater>();
            _collisionDetector = AllServices.Container.Single<ICollisionDetector>();
            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.UpdateCountOfLaser(_chargeCount);
        }

        private void PrepareGun()
        {
            if (_chargeCount >= MaxChargeCount) return;

            if (_chargeCount < MaxChargeCount && _currentLaserCooldown < LaserCoolDown)
            {
                _currentLaserCooldown += Time.deltaTime;
            }
            else
            {
                _chargeCount++;
                _currentLaserCooldown = 0;
                _spaceShipDataUpdater.UpdateCountOfLaser(_chargeCount);
            }
        }

        private void TryToShoot()
        {
            if (Input.GetKey(KeyCode.E))
                if (!_isShooting && _chargeCount > 0)
                    Shoot();

            if (_isShooting) DestroyReachedObjects();
        }

        private async void Shoot()
        {
            if (_isShooting) return;
            
            _isShooting = true;
            laserBeam.SetActive(true);
            _chargeCount--;
            _spaceShipDataUpdater.UpdateCountOfLaser(_chargeCount);

            await UniTask.Delay(TimeSpan.FromSeconds(LaserAttackTime));
            
            laserBeam.SetActive(false);
            _isShooting = false;
        }

        private void DestroyReachedObjects()
        {
            var collisionObjects = _collisionDetector.DetectCollisionsWithCapsule(tail.position, head.position);

            foreach (var reachedCollider in collisionObjects)
            {
                var destroyObject = reachedCollider.GetComponent<IDestroy>();

                if (destroyObject == null) continue;
                
                destroyObject.TryToDestroy();
                NotifyScore(reachedCollider);
                Destroy(reachedCollider.gameObject);
            }
        }

        private void NotifyScore(Collider reachedCollider)
        {
            Enum.TryParse(reachedCollider.tag, out EnemyTag enemyTag);
            _scoreUpdater.UpdateScore(enemyTag);
        }
    }
}
