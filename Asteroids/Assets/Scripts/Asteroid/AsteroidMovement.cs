using System;
using Infrastructure.Factories;
using UnityEngine;

namespace Asteroid
{
    public class AsteroidMovement : MonoBehaviour
    {
        [SerializeField] private AsteroidType asteroidType;
        
        private IAsteroidPositioner _asteroidPositioner;
        
        private float maxSpeed = 25;
        private Vector3 direction;

        private void Start()
        {
            _asteroidPositioner = PositionerFactory.GetAsteroidPositioner();
            
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
