using UnityEngine;

namespace Spaceship
{
    public class SpaceshipRotation : MonoBehaviour
    {
        private void FixedUpdate() => 
            RotateShip();

        private void RotateShip()
        {
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(transform.up, -180 * Time.deltaTime, Space.World);

            if (Input.GetKey(KeyCode.D))
                transform.Rotate(transform.up, 180 * Time.deltaTime, Space.World);
        }
    }
}
