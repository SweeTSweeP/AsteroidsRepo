using UnityEngine;
using Infrastructure.Services.ServiceLocator;

namespace Infrastructure.Services.AsteroidPositioner
{
    public interface IAsteroidPositioner : IService
    {
        Vector3 SpawnPosition();
        Vector3 RandomAngle(Transform transform);
    }
}