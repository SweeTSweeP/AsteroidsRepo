using UnityEngine;

namespace Spaceship
{
    public class SpaceshipMovement : MonoBehaviour
    {
        private const float MaxSpeed = 50;
        private const float Acceleration = 15;
        
        private float currentSpeed;
        private Vector3 _speedVector;

        private void Start() => 
            _speedVector.Set(0, 0, 0);

        private void Update() => 
            MoveShip();

        private void MoveShip()
        {
            if (Input.GetKey(KeyCode.W))
            {
                currentSpeed += Acceleration * Time.deltaTime;
                currentSpeed = currentSpeed > MaxSpeed ? MaxSpeed : currentSpeed;

                _speedVector += transform.forward * (Acceleration * Time.deltaTime);
            }

            var length = _speedVector.magnitude;

            if (length > MaxSpeed)
            {
                _speedVector.Normalize();
                _speedVector *= MaxSpeed;
            }

            transform.Translate(_speedVector * Time.deltaTime, Space.World);
        }
    }
}
