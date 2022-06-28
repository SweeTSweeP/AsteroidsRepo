using UnityEngine;

namespace Infrastructure.Services.Collisions
{
    public interface ICollisionDetector : IService
    {
        Collider DetectCollisionsWithSphere(Transform transform, float radius = 0.1f);
        Collider[] DetectCollisionsWithCapsule(Vector3 tail, Vector3 head, float radius = 0.5f);
    }
}