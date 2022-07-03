using UnityEngine;
using Infrastructure.Services.ServiceLocator;

namespace Infrastructure.Services.Loaders.AssetLoad
{
    public interface IAssetLoader : IService
    {
        GameObject LoadAsset(string assetName);
    }
}