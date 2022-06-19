using UnityEngine;

namespace Teleport
{
    [RequireComponent(typeof(Renderer))]
    public class Teleport : MonoBehaviour
    {
        private new Renderer renderer;
        private Vector3 _areaTopRightCorner;
        private float _positionModificator = 0.975f;

        private void Start()
        {
            renderer = GetComponent<Renderer>();
            _areaTopRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));
        }

        private void Update()
        {
            if (!renderer.isVisible)
                HyperJump();
        }

        private void HyperJump()
        {
            var currentPosition = Vector3.zero;

            if (transform != null && Camera.main != null)
                currentPosition = Camera.main.WorldToViewportPoint(transform.position);

            if (DiagonalTeleportation(currentPosition)) return;

            if (LeftRightTeleportation(currentPosition)) return;

            if (TopDownTeleportation(currentPosition)) return;
        }

        private bool DiagonalTeleportation(Vector3 currentPosition)
        {
            if (currentPosition.x >= 1f && currentPosition.y >= 1f)
            {
                transform.position = new Vector3(-_areaTopRightCorner.x * _positionModificator, 0, -_areaTopRightCorner.z * _positionModificator);
                return true;
            }

            if (currentPosition.x <= 0f && currentPosition.y <= 0f)
            {
                transform.position = new Vector3(_areaTopRightCorner.x * _positionModificator, 0, _areaTopRightCorner.z * _positionModificator);
                return true;
            }

            if (currentPosition.x >= 1f && currentPosition.y <= 0f)
            {
                transform.position = new Vector3(-_areaTopRightCorner.x * _positionModificator, 0, _areaTopRightCorner.z * _positionModificator);
                return true;
            }

            if (currentPosition.x <= 0f && currentPosition.y >= 1f)
            {
                transform.position = new Vector3(_areaTopRightCorner.x * _positionModificator, 0, -_areaTopRightCorner.z * _positionModificator);
                return true;
            }

            return false;
        }

        private bool LeftRightTeleportation(Vector3 currentPosition)
        {
            if (currentPosition.x >= 1f)
            {
                transform.position = new Vector3(-_areaTopRightCorner.x * _positionModificator, 0, transform.position.z);
                return true;
            }

            if (currentPosition.x <= 0f)
            {
                transform.position = new Vector3(_areaTopRightCorner.x * _positionModificator, 0, transform.position.z);
                return true;
            }

            return false;
        }

        private bool TopDownTeleportation(Vector3 currentPosition)
        {
            if (currentPosition.y >= 0.95f)
            {
                transform.position =
                    new Vector3(transform.position.x, 0, -_areaTopRightCorner.z * _positionModificator);
                return true;
            }

            if (currentPosition.y <= 0.05f)
            {
                transform.position = new Vector3(transform.position.x, 0, _areaTopRightCorner.z * _positionModificator);
                return true;
            }

            return false;
        }
    }
}