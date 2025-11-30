using Data;
using Gameplay.Dungeon.Rooms.Shrine;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "ShrineRoomVariantData", menuName = "Gameplay/Dungeon/Data/ShrineRoomVariantData")] 
    public class ShrineRoomVariantData : RoomVariantData
    {
        [SerializeField] private ShrineBuffsConfig _config;
        public ShrineBuffsConfig Config => _config;
        
        public override RoomType RoomType => RoomType.Shrine;
    }
}