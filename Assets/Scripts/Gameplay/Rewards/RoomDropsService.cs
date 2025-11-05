using AssetManagement.AssetProviders.Core;
using Cysharp.Threading.Tasks;
using Gameplay.Configs;
using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.RoomTypes;

namespace Gameplay.Rewards
{
    public class RoomDropsService
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;
        private readonly RewardDistributor _rewardDistributor;
        private readonly RewardSelectorService _rewardSelectorService;

        public RoomDropsService(RewardSelectorService rewardSelectorService, RewardDistributor rewardDistributor,
            BaseConfigProvider<GameplayConfig> configProvider)
        {
            _rewardSelectorService = rewardSelectorService;
            _rewardDistributor = rewardDistributor;
            _configProvider = configProvider;
        }

        public async UniTask GiveRewardToPlayer(DungeonRoom room)
        {
            var roomType = room.RoomType;
            var config = _configProvider.GetConfig<DungeonRoomsDatabase>();

            if (!config.TryGetRoomData(roomType, out var roomData))
                return;

            var reward = _rewardSelectorService.SelectReward(roomData.RewardDropTable);

            await _rewardDistributor.GiveRewardToPlayer(reward);
        }
    }
}