using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Loaders
{
    public class AssetLoader : IAssetLoader 
    {
        public GameObject LoadAsset(string assetName) =>
            Addressables.LoadAssetAsync<GameObject>(assetName).WaitForCompletion();
    }

    public interface IAssetLoader
    {
        GameObject LoadAsset(string assetName);
    }
}