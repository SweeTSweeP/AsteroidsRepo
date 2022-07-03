using Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Infrastructure.Services.LevelClean
{
    public interface ILevelCleaner : IService
    {
        void AddObjectToCollector(GameObject gameObject);
        void CleanCollector();
    }
}