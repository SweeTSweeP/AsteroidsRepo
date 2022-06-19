using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyPositioner : IEnemyPositioner
    {
        private float _positionModificator = 0.92f;

        private Corner corner;

        public Vector3 GetDirection(Corner corner) =>
            corner switch
            {
                Corner.UpRight => new Vector3(-1, 0, 0),
                Corner.UpLeft => new Vector3(1, 0, 0),
                Corner.DownRight => new Vector3(-1, 0, 0),
                Corner.DownLeft => new Vector3(1, 0, 0),
            };

        public Vector3 GetPosition(Corner corner) =>
            corner switch
            {
                Corner.UpRight => new Vector3(1 * _positionModificator, 1 * _positionModificator, 0),
                Corner.UpLeft => new Vector3(0, 1 * _positionModificator, 0),
                Corner.DownRight => new Vector3(1 * _positionModificator, 1 - _positionModificator, 0),
                Corner.DownLeft => new Vector3(0, 1 - _positionModificator, 0)
            };

        public Corner RandomCorner()
        {
            var corners = Enum.GetValues(typeof(Corner));
            return (Corner)corners.GetValue(Random.Range(0, corners.Length));
        }
    }
}