using System;
using System.Collections.Generic;
using Data;
using Gameplay.Dungeon.Rooms;

namespace Helpers
{
    public static class RoomTypeHelper
    {
        private static readonly Dictionary<RoomType, Type> _typeDict = new()
        {
            {RoomType.Corridor, typeof(CorridorRoom)},
            {RoomType.Decision, typeof(DecisionRoom)},
            {RoomType.TreasureChest, typeof(TreasureChestRoom)},
            {RoomType.BasicFight, typeof(BasicFightRoom)},
            {RoomType.EliteFight, typeof(EliteFightRoom)},
            {RoomType.BossFight, typeof(BossFightRoom)},
            {RoomType.Shop, typeof(ShopRoom)},
            {RoomType.Shrine, typeof(ShrineRoom)},
            {RoomType.PhysicalMaster, typeof(PhysicalMasterRoom)},
            {RoomType.MagicMaster, typeof(MagicMasterRoom)},
            {RoomType.Trap, typeof(TrapRoom)},
            {RoomType.EncounterBattle, typeof(EncounterBattleRoom)},
        };

        public static bool IsRoomValidForSelection(RoomType roomType)
        {
            if(EncounterRoomTypes.Contains(roomType))
                return false;
            
            if (SpecialRoomTypes.Contains(roomType))
                return false;
            
            if(roomType is RoomType.Corridor)
                return false;

            return true;
        }
        
        public static readonly List<RoomType> EncounterRoomTypes = new()
        {
            RoomType.Trap,
            RoomType.EncounterBattle,
        };

        public static readonly List<RoomType> SpecialRoomTypes = new()
        {
            RoomType.BossFight,
            RoomType.Decision
        };
        
        public static Type GetExpectedType(RoomType roomType) => _typeDict[roomType];
    }
}