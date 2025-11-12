using System;
using Cysharp.Threading.Tasks;

namespace Gameplay.Rewards
{
    public class ItemsDistributor
    {
        private readonly PurchaseDistributionStrategy _purchaseDistributionStrategy;
        private readonly LootDistributionStrategy _lootDistributionStrategy;
        
        public ItemsDistributor(PurchaseDistributionStrategy purchaseStrategy, 
            LootDistributionStrategy lootDistributionStrategy)
        {
            _purchaseDistributionStrategy = purchaseStrategy;
            _lootDistributionStrategy = lootDistributionStrategy;
        }
        
        public async UniTask GiveRewardToPlayer(BaseGameItem item, int amount, RewardContext rewardContext)
        {
            switch (rewardContext)
            {
                case RewardContext.Loot:
                    await _lootDistributionStrategy.DistributeItem(item, amount);
                    break;
                case RewardContext.Purchase:
                    await _purchaseDistributionStrategy.DistributeItem(item, amount);
                    break;
                default:
                    throw new Exception("Unhandled reward context");
            }
        }
    }
}