using UnityEngine;

namespace Teleport
{
    public class Teleport : MonoBehaviour
    {
        private void OnBecameInvisible() =>
            HyperJump();
        
        private void HyperJump()
        {
            var newPosition = Vector3.zero;
            var currentPosition = Camera.main.WorldToViewportPoint(transform.position);

            if (currentPosition.x is >= 1 or <= 0)
                newPosition = new Vector3(-transform.position.x, newPosition.y, transform.position.z);

            if (currentPosition.y is >= 1 or <= 0)
                newPosition = new Vector3(transform.position.x, newPosition.y, -transform.position.z);
            
            if(currentPosition.x is >=1 or <=0 && currentPosition.y is >= 1 or <= 0)
                newPosition = new Vector3(-transform.position.x, newPosition.y, -transform.position.z);

            transform.position = newPosition;
        }
    }
}