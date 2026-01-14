using System.Collections.Generic;
using Data.Constants;
using Gameplay.Configs;
using Gameplay.Dungeon.RoomVariants;
using UnityEngine;

namespace Gameplay.Dungeon
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonConfigs + "DungeonRoomsDatabase")]
    public class DungeonRoomsDatabase : GameplayConfig
    {
        [SerializeField] private List<RoomVariantData> _rooms;

        public List<RoomVariantData> Rooms => _rooms;
    }
}