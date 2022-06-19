using Infrastructure.Factories;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private const float Speed = 50;

        private IEnemyPositioner _enemyPositioner;

        private Corner _corner;
        private Vector3 _startPosition;
        private Vector3 _direction;

        private void Start()
        {
            InitPositioner();
            InitPosition();
        }

        private void FixedUpdate() => 
            MoveEnemy();

        private void MoveEnemy() => 
            transform.position += _direction * Speed * Time.deltaTime;

        private void InitPosition()
        {
            _corner = _enemyPositioner.RandomCorner();
            _startPosition = _enemyPositioner.GetPosition(_corner);
            _direction = _enemyPositioner.GetDirection(_corner);

            var newPosition = Camera.main.ViewportToWorldPoint(_startPosition);

            transform.position = new Vector3(newPosition.x, 0, newPosition.z);
        }

        private void InitPositioner()
        {
            _enemyPositioner = PositionerFactory.GetEnemyPositioner();
        }
    }
}
