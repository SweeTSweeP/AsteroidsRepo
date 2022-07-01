using System;
using UnityEngine;

namespace Infrastructure.Services.SpaceShipDataUpdate
{
    public class SpaceShipDataUpdater : ISpaceShipDataUpdater
    {
        public event Action<Vector3> CoordinatesUpdated;
        public event Action<Vector3> AngleUpdated;
        public event Action<float> AccelerationUpdated;
        public event Action<int> CountOfLaserUpdated;
        public event Action<int> CountOfBulletsUpdated;

        public void UpdateCoordinates(Vector3 coordinates) => 
            CoordinatesUpdated?.Invoke(coordinates);

        public void UpdateAngle(Vector3 angle) =>
            AngleUpdated?.Invoke(angle);

        public void UpdateAcceleration(float acceleration) =>
            AccelerationUpdated?.Invoke(acceleration);

        public void UpdateCountOfLaser(int countOfLaser) =>
            CountOfLaserUpdated?.Invoke(countOfLaser);

        public void UpdateCountOfBullets(int countOfBullets) =>
            CountOfBulletsUpdated?.Invoke(countOfBullets);
    }
}