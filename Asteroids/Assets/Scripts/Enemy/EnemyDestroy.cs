using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyDestroy : MonoBehaviour, IDestroy
    {
        public event Action<Vector3> ObjectDestroyed;

        public void TryToDestroy() => ObjectDestroyed?.Invoke(transform.position);
    }
}