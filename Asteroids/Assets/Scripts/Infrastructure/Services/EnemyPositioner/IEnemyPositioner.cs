using Infrastructure.Enums;
using Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Infrastructure.Services.EnemyPositioner
{
    public interface IEnemyPositioner : IService
    {
        Vector3 GetDirection(Corner corner);
        Vector3 GetPosition(Corner corner);
        Quaternion GetRotation(Corner corner);
        Corner RandomCorner();
    }
}