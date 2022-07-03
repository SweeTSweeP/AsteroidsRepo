using System;
using Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Infrastructure.Services.SpaceShipDataUpdate
{
    public interface ISpaceShipDataUpdater : IService
    {
        event Action<Vector3> CoordinatesUpdated;
        event Action<Vector3> AngleUpdated;
        event Action<float> AccelerationUpdated;
        event Action<int> CountOfLaserUpdated;
        event Action<int> CountOfBulletsUpdated;
        void UpdateCoordinates(Vector3 coordinates);
        void UpdateAngle(Vector3 angle);
        void UpdateAcceleration(float acceleration);
        void UpdateCountOfLaser(int countOfLaser);
        void UpdateCountOfBullets(int countOfBullets);
    }
}