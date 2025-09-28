using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms;
using Gameplay.Services;

namespace Gameplay.Rewards
{
    public class RoomDropsService
    {
        private readonly RewardSelectorService _rewardSelectorService;
        private readonly RewardDistributor _rewardDistributor;
        private readonly DungeonFactory _dungeonFactory;

        public RoomDropsService(RewardSelectorService rewardSelectorService, RewardDistributor rewardDistributor, DungeonFactory dungeonFactory)
        {
            _rewardSelectorService = rewardSelectorService;
            _rewardDistributor = rewardDistributor;
            _dungeonFactory = dungeonFactory;
        }

        public async UniTask GiveRewardToPlayer(DungeonRoom room)
        {
            var roomType = room.RoomType;
            var roomData = _dungeonFactory.GetRoomData(roomType);
            
            var reward = _rewardSelectorService.SelectReward(roomData.RewardDropTable);

            await _rewardDistributor.GiveRewardToPlayer(reward);
        }
    }
}