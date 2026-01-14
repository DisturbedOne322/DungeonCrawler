using System.Collections.Generic;
using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "Trap Room Variant Data")]
    public class TrapRoomVariantData : EncounterRoomVariantData
    {
        [SerializeField] private List<GameObject> _trapPrefabs;
        public override RoomType RoomType => RoomType.Trap;
        public IReadOnlyList<GameObject> TrapPrefabs => _trapPrefabs;
    }
}