using System;
using Enemy;
using Infrastructure.Enums;
using UnityEngine;

namespace Asteroid
{
    public class AsteroidDestroy : MonoBehaviour, IDestroy
    {
        [SerializeField] private AsteroidType asteroidType;

        public event Action<Vector3, AsteroidType> ObjectDestroyed;

        public void TryToDestroy() => ObjectDestroyed?.Invoke(transform.position, asteroidType);
    }
}