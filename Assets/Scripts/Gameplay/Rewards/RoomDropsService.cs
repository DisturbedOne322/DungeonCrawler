using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms;

namespace Gameplay.Rewards
{
    public class RoomDropsService
    {
        private readonly ItemsDistributor _itemsDistributor;
        private readonly RewardSelectorService _rewardSelectorService;

        public RoomDropsService(RewardSelectorService rewardSelectorService, ItemsDistributor itemsDistributor)
        {
            _rewardSelectorService = rewardSelectorService;
            _itemsDistributor = itemsDistributor;
        }

        public async UniTask GiveRewardToPlayer(DungeonRoom room)
        {
            var reward = _rewardSelectorService.SelectReward(room.RoomData.RewardDropTable);

            if(reward == null)
                return;
            
            await _itemsDistributor.GiveRewardToPlayer(reward.Item, reward.Amount, ItemObtainContext.Loot);
        }
    }
}