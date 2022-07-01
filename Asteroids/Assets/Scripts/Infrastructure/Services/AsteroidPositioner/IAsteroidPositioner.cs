using UnityEngine;

namespace Infrastructure.Services.AsteroidPositioner
{
    public interface IAsteroidPositioner : IService
    {
        Vector3 SpawnPosition();
        Vector3 RandomAngle(Transform transform);
    }
}