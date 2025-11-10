using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Configs;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "DungeonRoomsDatabase", menuName = "Gameplay/Dungeon/Data/DungeonRoomsDatabase")]
    public class DungeonRoomsDatabase : GameplayConfig
    {
        [SerializeField] private List<DungeonRoom> _roomDataList;

        private Dictionary<RoomType, DungeonRoom> _roomDataMap;

        private void OnEnable()
        {
            _roomDataMap = _roomDataList.ToDictionary(d => d.RoomType, d => d);
        }

        public bool TryGetRoom(RoomType type, out DungeonRoom roomData)
        {
            if (_roomDataMap.TryGetValue(type, out roomData))
                return true;

            throw new KeyNotFoundException($"No room data found for type {type}");
        }
    }
}