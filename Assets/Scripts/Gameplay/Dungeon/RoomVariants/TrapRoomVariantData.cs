using System.Collections.Generic;
using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "Trap Room Variant Data")]
    public class TrapRoomVariantData : EncounterRoomVariantData
    {
        [SerializeField] private List<GameObject> _trapPrefabs;
        public IReadOnlyList<GameObject> TrapPrefabs => _trapPrefabs;
        
        public override RoomType RoomType => RoomType.Trap;
    }
}