using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.Loaders.AssetLoad
{
    public class AssetLoader : IAssetLoader 
    {
        public GameObject LoadAsset(string assetName) =>
            Addressables.LoadAssetAsync<GameObject>(assetName).WaitForCompletion();
    }
}