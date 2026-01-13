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
            {RoomType.RandomEncounter, typeof(RandomEncounterRoom)},
        };
        
        public static Type GetExpectedType(RoomType roomType) => _typeDict[roomType];
    }
}