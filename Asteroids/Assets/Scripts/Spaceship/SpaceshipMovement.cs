using UnityEngine;

namespace Spaceship
{
    public class SpaceshipMovement : MonoBehaviour
    {
        private const float IncreaseSpeedCoefficient = 0.5f;
        private const float DecreaseSpeedCoefficient = 0.3f;
        
        [SerializeField] private float speed;

        private float _currentSpeed;

        private void FixedUpdate() => 
            MoveShip();

        private void MoveShip()
        {
            ControlSpeed();

            transform.position += transform.forward * _currentSpeed * Time.deltaTime;
            
            CompensateSpeed();
        }

        private void ControlSpeed()
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (_currentSpeed < speed)
                    _currentSpeed += IncreaseSpeedCoefficient;
            }
            else
            {
                if (_currentSpeed > 0)
                    _currentSpeed -= DecreaseSpeedCoefficient;
            }
        }

        private void CompensateSpeed()
        {
            if (_currentSpeed < 0)
                _currentSpeed = 0;
        }
    }
}
