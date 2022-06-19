using UnityEngine;

namespace Asteroid
{
    public interface IAsteroidPositioner
    {
        Vector3 SpawnPosition();
        Vector3 RandomAngle(Transform transform);
    }
}