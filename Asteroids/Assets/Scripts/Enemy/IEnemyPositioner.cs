using UnityEngine;

namespace Enemy
{
    public interface IEnemyPositioner
    {
        Vector3 GetDirection(Corner corner);
        Vector3 GetPosition(Corner corner);
        Corner RandomCorner();
    }
}