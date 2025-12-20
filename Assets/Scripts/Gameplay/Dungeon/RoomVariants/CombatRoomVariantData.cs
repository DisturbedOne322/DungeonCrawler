using System.Collections.Generic;
using Data;
using Gameplay.Dungeon.Rooms;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "CombatRoomVariantData", menuName = "Gameplay/Dungeon/Data/CombatRoomVariantData")]
    public class CombatRoomVariantData : RoomVariantData
    {
        [SerializeField] private List<EnemyUnitData> _enemiesSelection;
        
        public List<EnemyUnitData> EnemiesSelection => _enemiesSelection;
        
        public override RoomType RoomType => RoomType.Combat;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var combatRoom = room as CombatRoom;
            combatRoom?.SetData(this);
        }
    }
}