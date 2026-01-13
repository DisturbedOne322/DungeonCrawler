using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "RandomEncounterRoomVariantData")]
    public class RandomEncounterRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType { get; }
    }
}