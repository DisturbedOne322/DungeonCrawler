using Data;
using Gameplay.Rewards;
using UnityEngine;

namespace Gameplay.Dungeon.RoomTypes
{
    public abstract class DungeonRoom : MonoBehaviour
    {
        [SerializeField] private Transform _playerStandPoint;
        [SerializeField] private RewardDropTable _rewardDropTable;

        public Transform PlayerStandPoint => _playerStandPoint;
        public RewardDropTable RewardDropTable => _rewardDropTable;

        public abstract RoomType RoomType { get; }

        public virtual void ResetRoom()
        {
        }

        public virtual void SetupRoom()
        {
        }
    }
}