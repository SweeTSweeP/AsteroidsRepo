using Infrastructure.Services.Collisions;
using Infrastructure.Services.Player;
using Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Enemy
{
    public class PlayerDamage : MonoBehaviour
    {
        private ICollisionDetector _collisionDetector;
        private IPlayerDeathIndicator _playerDeathIndicator;

        private void Start()
        {
            _collisionDetector = AllServices.Container.Single<ICollisionDetector>();
            _playerDeathIndicator = AllServices.Container.Single<IPlayerDeathIndicator>();
        }

        private void Update() => 
            TryToDestroyPlayer();

        private void TryToDestroyPlayer()
        {
            var collisionObject = _collisionDetector.DetectCollisionsWithSphere(transform, 1.5f);

            if (collisionObject != null && collisionObject.CompareTag(Constants.PlayerTag))
            {
                Destroy(collisionObject.transform.root.gameObject);
                _playerDeathIndicator.PlayerDie();
            }
        }
    }
}