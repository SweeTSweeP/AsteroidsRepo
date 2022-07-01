using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.LevelClean
{
    public class LevelCleaner : ILevelCleaner
    {
        private List<GameObject> _levelObjectCollector;

        public void AddObjectToCollector(GameObject gameObject)
        {
            _levelObjectCollector ??= new List<GameObject>();

            _levelObjectCollector.Add(gameObject);
        }

        public void CleanCollector()
        {
            foreach (var gameObject in _levelObjectCollector) Object.Destroy(gameObject);

            _levelObjectCollector.Clear();
        }
    }
}