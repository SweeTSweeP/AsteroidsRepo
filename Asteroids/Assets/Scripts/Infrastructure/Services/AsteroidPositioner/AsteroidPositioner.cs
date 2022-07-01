using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Services.AsteroidPositioner
{
    public class AsteroidPositioner : IAsteroidPositioner
    {
        public Vector3 SpawnPosition()
        {
            GetPosition(out var spawnPosition, out var overlappedObjects);

            while (overlappedObjects.Count > 0) GetPosition(out spawnPosition, out overlappedObjects);

            return spawnPosition;
        }
        
        public Vector3 RandomAngle(Transform transform)
        {
            transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            
            return transform.TransformDirection(Vector3.forward);
        }

        private void GetPosition(out Vector3 spawnPosition, out List<Collider> overlappedObjects)
        {
            var xPosition = Random.Range(Screen.width * 0.1f, Screen.width - Screen.width * 0.1f);
            var yPosition = Random.Range(Screen.height * 0.1f, Screen.height - Screen.height * 0.1f);

            var rawSpawnPosition = new Vector3(xPosition, yPosition, 0);

            spawnPosition = Camera.main.ScreenToWorldPoint(rawSpawnPosition);
            spawnPosition.y = 0;

            var hitColliders = new Collider[2];
            Physics.OverlapSphereNonAlloc(spawnPosition, 5, hitColliders);

            overlappedObjects = hitColliders.Where(s => s != null).ToList();
        }
    }
}