using System.Collections.Generic;
using Gameplay.Configs;
using Gameplay.Dungeon.RoomVariants;
using UnityEngine;

namespace Gameplay.Dungeon
{
    [CreateAssetMenu(fileName = "DungeonRoomsDatabase", menuName = "Gameplay/Dungeon/Data/DungeonRoomsDatabase")]
    public class DungeonRoomsDatabase : GameplayConfig
    {
        [SerializeField] private List<RoomVariantData> _rooms;
        
        public List<RoomVariantData> Rooms => _rooms;
    }
}