using Infrastructure.Services;
using Infrastructure.Services.AsteroidPositioner;
using UnityEngine;

namespace Asteroid
{
    public class AsteroidMovement : MonoBehaviour
    {
        [SerializeField] private AsteroidType asteroidType;
        
        private IAsteroidPositioner _asteroidPositioner;

        private float maxSpeed = 12.5f;
        private Vector3 direction;

        private void Start()
        {
            _asteroidPositioner = AllServices.Container.Single<IAsteroidPositioner>();
            
            if (asteroidType == AsteroidType.Parent) transform.position = _asteroidPositioner.SpawnPosition();
            
            direction = _asteroidPositioner.RandomAngle(transform);

            if (asteroidType == AsteroidType.Child) maxSpeed *= 2;
        }

        private void FixedUpdate() => 
            MoveAsteroid();

        private void MoveAsteroid() => 
            transform.position += direction * (maxSpeed * Time.deltaTime);
    }
}
