using AssetManagement.AssetProviders;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.RoomTypes;

namespace Gameplay.Rewards
{
    public class RoomDropsService
    {
        private readonly DungeonRoomsDatabase _dungeonRoomsDatabase;
        private readonly ItemsDistributor _itemsDistributor;
        private readonly RewardSelectorService _rewardSelectorService;

        public RoomDropsService(RewardSelectorService rewardSelectorService, ItemsDistributor itemsDistributor,
            GameplayConfigsProvider configsProvider)
        {
            _rewardSelectorService = rewardSelectorService;
            _itemsDistributor = itemsDistributor;

            _dungeonRoomsDatabase = configsProvider.GetConfig<DungeonRoomsDatabase>();
        }

        public async UniTask GiveRewardToPlayer(DungeonRoom room)
        {
            var roomType = room.RoomType;

            if (!_dungeonRoomsDatabase.TryGetRoom(roomType, out var roomData))
                return;

            var reward = _rewardSelectorService.SelectReward(roomData.RewardDropTable);

            if(reward == null)
                return;
            
            await _itemsDistributor.GiveRewardToPlayer(reward.Item, reward.Amount, RewardContext.Loot);
        }
    }
}