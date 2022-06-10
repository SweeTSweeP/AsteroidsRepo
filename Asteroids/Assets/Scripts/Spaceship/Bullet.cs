using System;
using UnityEngine;

namespace Spaceship
{
    public class Bullet : MonoBehaviour
    {
        private const float Speed = 200;

        private Vector3 direction;

        private void OnEnable() => 
            direction = transform.TransformDirection(Vector3.forward);

        private void FixedUpdate() => 
            transform.position +=  direction * Speed * Time.deltaTime;
    }
}