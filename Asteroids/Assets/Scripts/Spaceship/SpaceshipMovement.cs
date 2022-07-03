using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.SpaceShipDataUpdate;
using UnityEngine;

namespace Spaceship
{
    public class SpaceshipMovement : MonoBehaviour
    {
        private const float MaxSpeed = 50;
        private const float Acceleration = 15;

        private ISpaceShipDataUpdater _spaceShipDataUpdater;
        
        private float _currentSpeed;
        private Vector3 _speedVector;

        private void Start()
        {
            _speedVector.Set(0, 0, 0);
            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
        }

        private void Update() => 
            MoveShip();

        private void MoveShip()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _currentSpeed += Acceleration * Time.deltaTime;
                _currentSpeed = _currentSpeed > MaxSpeed ? MaxSpeed : _currentSpeed;

                _speedVector += transform.forward * (Acceleration * Time.deltaTime);
            }
            else if (_currentSpeed > 0 && !Input.GetKey(KeyCode.W))
            {
                _currentSpeed -= Acceleration * Time.deltaTime;
                _currentSpeed = _currentSpeed < 0 ? 0 : _currentSpeed;

                _speedVector += transform.forward * (-Acceleration / 4 * Time.deltaTime);
            }

            var length = _speedVector.magnitude;

            if (length > MaxSpeed)
            {
                _speedVector.Normalize();
                _speedVector *= MaxSpeed;
            }

            transform.Translate(_speedVector * Time.deltaTime, Space.World);
            
            SendData();
        }

        private void SendData()
        {
            _spaceShipDataUpdater.UpdateAcceleration(_currentSpeed);
            _spaceShipDataUpdater.UpdateCoordinates(transform.position);
        }
    }
}
