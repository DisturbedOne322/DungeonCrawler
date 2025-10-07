using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Configs;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "DungeonRoomsDatabase", menuName = "Gameplay/Dungeon/Data/DungeonRoomsDatabase")]
    public class DungeonRoomsDatabase : GameplayConfig
    {
        [SerializeField] private List<RoomData> _roomDataList;

        private Dictionary<RoomType, RoomData> _roomDataMap;

        private void OnEnable()
        {
            _roomDataMap = _roomDataList
                .Where(d => d?.RoomPrefab != null)
                .ToDictionary(d => d.RoomPrefab.RoomType, d => d);
        }

        public bool TryGetRoomData(RoomType type, out RoomData roomData)
        {
            if(_roomDataMap.TryGetValue(type, out roomData))
                return true;
            
            throw new KeyNotFoundException($"No room data found for type {type}");
        }
    }
}