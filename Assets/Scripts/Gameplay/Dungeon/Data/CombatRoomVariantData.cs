using System.Collections.Generic;
using Data;
using Gameplay.Dungeon.RoomTypes;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "CombatRoomVariantData", menuName = "Gameplay/Dungeon/Data/CombatRoomVariantData")]
    public class CombatRoomVariantData : RoomVariantData
    {
        [SerializeField] private List<EnemyUnitData> _enemiesSelection;
        
        public override RoomType RoomType => RoomType.Combat;
    }
}