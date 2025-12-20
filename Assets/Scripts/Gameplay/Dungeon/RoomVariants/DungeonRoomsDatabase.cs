using System.Collections.Generic;
using Gameplay.Configs;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "DungeonRoomsDatabase", menuName = "Gameplay/Dungeon/Data/DungeonRoomsDatabase")]
    public class DungeonRoomsDatabase : GameplayConfig
    {
        [SerializeField] private List<RoomVariantData> _rooms;
        
        public List<RoomVariantData> Rooms => _rooms;
    }
}