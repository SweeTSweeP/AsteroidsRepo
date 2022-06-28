using System.Linq;
using UnityEngine;

namespace Infrastructure.Services.Collisions
{
    public class CollisionDetector : ICollisionDetector
    {
        public Collider DetectCollisionsWithSphere(Transform transform, float radius = 0.1f)
        {
            var hitColliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(transform.position, radius, hitColliders);

            return hitColliders[0] != null ? hitColliders[0] : null;
        }

        public Collider[] DetectCollisionsWithCapsule(Vector3 tail, Vector3 head, float radius = 0.5f)
        {
            var hitColliders = new Collider[10];
            Physics.OverlapCapsuleNonAlloc(tail, head, radius, hitColliders);

            return hitColliders.Where(s => s != null).ToArray();
        }
    }
}