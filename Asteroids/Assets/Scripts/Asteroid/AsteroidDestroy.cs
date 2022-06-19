using System;
using UnityEngine;

namespace Asteroid
{
    public class AsteroidDestroy : MonoBehaviour
    {
        [SerializeField] private AsteroidType _asteroidType;
        
        public event Action<Vector3, AsteroidType> ObjectDestroyed;
        
        public void TryToDestroy() => ObjectDestroyed?.Invoke(transform.position, _asteroidType);
    }
}