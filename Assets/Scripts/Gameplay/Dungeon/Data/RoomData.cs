using Gameplay.Dungeon.Rooms;
using Gameplay.Rewards;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "Gameplay/Dungeon/Data/RoomData")]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private DungeonRoom _prefab;
        [SerializeField] private Texture2D _icon;
        [SerializeField] private RewardDropTable _rewardDropTable;
        
        public DungeonRoom RoomPrefab => _prefab;
        public Texture2D Icon => _icon;
        public RewardDropTable RewardDropTable => _rewardDropTable;
    }
}