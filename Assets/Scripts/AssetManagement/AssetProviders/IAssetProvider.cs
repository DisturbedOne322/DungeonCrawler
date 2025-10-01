using Cysharp.Threading.Tasks;

namespace AssetManagement.AssetProviders
{
    public interface IAssetProvider
    {
        UniTask Initialize();
        UniTask<T> LoadAsset<T>(string assetName);
        void Dispose();
    }
}