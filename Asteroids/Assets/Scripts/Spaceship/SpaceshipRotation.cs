using System;
using UnityEngine;

namespace Spaceship
{
    public class SpaceshipRotation : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private void FixedUpdate() => 
            RotateShip();

        private void RotateShip()
        {
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(-Vector3.up * rotationSpeed);

            if (Input.GetKey(KeyCode.D))
                transform.Rotate(Vector3.up * rotationSpeed);
        }
    }
}
