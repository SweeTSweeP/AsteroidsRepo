using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.SpaceShipDataUpdate;
using UnityEngine;

namespace Spaceship
{
    public class SpaceshipRotation : MonoBehaviour
    {
        private ISpaceShipDataUpdater _spaceShipDataUpdater;

        private void Start()
        {
            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.UpdateAngle(transform.rotation.eulerAngles);
        }

        private void FixedUpdate() => 
            RotateShip();

        private void RotateShip()
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up, -180 * Time.deltaTime, Space.World);
                _spaceShipDataUpdater.UpdateAngle(transform.rotation.eulerAngles);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.up, 180 * Time.deltaTime, Space.World);
                _spaceShipDataUpdater.UpdateAngle(transform.rotation.eulerAngles);
            }
        }
    }
}
