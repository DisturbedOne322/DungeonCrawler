using Cysharp.Threading.Tasks;

namespace AssetManagement.AssetProviders.Core
{
    public interface ICustomInitializable
    {
        UniTask Initialize();
    }
}