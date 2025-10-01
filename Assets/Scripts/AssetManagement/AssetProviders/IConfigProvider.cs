namespace AssetManagement.AssetProviders
{
    public interface IConfigProvider<TType> : IAssetProvider
    {
        T GetAsset<T>() where T : TType;
    }
}