using Cysharp.Threading.Tasks;

namespace Gameplay.Rewards
{
    public interface IItemDistributionStrategy
    {
        UniTask DistributeItem(BaseGameItem item, int amount);
    }
}