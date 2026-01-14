using System;
using System.Collections.Generic;
using Data;
using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.RoomVariants;

namespace Helpers
{
    public static class RoomTypeHelper
    {
        private static readonly Dictionary<RoomType, Type> _roomTypeDict = new()
        {
            { RoomType.Corridor, typeof(CorridorRoom) },
            { RoomType.Decision, typeof(DecisionRoom) },
            { RoomType.TreasureChest, typeof(TreasureChestRoom) },
            { RoomType.BasicFight, typeof(BasicFightRoom) },
            { RoomType.EliteFight, typeof(EliteFightRoom) },
            { RoomType.BossFight, typeof(BossFightRoom) },
            { RoomType.Shop, typeof(ShopRoom) },
            { RoomType.Shrine, typeof(ShrineRoom) },
            { RoomType.PhysicalMaster, typeof(PhysicalMasterRoom) },
            { RoomType.MagicMaster, typeof(MagicMasterRoom) },
            { RoomType.Trap, typeof(TrapRoom) },
            { RoomType.EncounterBattle, typeof(EncounterBattleRoom) }
        };

        private static readonly Dictionary<RoomType, Type> _roomDataTypeDict = new()
        {
            { RoomType.Corridor, typeof(CorridorRoomVariantData) },
            { RoomType.Decision, typeof(DecisionRoomVariantData) },
            { RoomType.TreasureChest, typeof(TreasureChestRoomVariantData) },
            { RoomType.BasicFight, typeof(BasicFightRoomVariantData) },
            { RoomType.EliteFight, typeof(EliteFightRoomVariantData) },
            { RoomType.BossFight, typeof(BossFightRoomVariantData) },
            { RoomType.Shop, typeof(ShopRoomVariantData) },
            { RoomType.Shrine, typeof(ShrineRoomVariantData) },
            { RoomType.PhysicalMaster, typeof(PhysicalMasterRoomVariantData) },
            { RoomType.MagicMaster, typeof(MagicMasterRoomVariantData) },
            { RoomType.Trap, typeof(TrapRoomVariantData) },
            { RoomType.EncounterBattle, typeof(EncounterBattleRoomVariantData) }
        };

        public static readonly List<RoomType> EncounterRoomTypes = new()
        {
            RoomType.Trap,
            RoomType.EncounterBattle
        };

        public static readonly List<RoomType> SpecialRoomTypes = new()
        {
            RoomType.BossFight,
            RoomType.Decision
        };

        public static bool IsRoomValidForSelection(RoomType roomType)
        {
            if (EncounterRoomTypes.Contains(roomType))
                return false;

            if (SpecialRoomTypes.Contains(roomType))
                return false;

            if (roomType is RoomType.Corridor)
                return false;

            return true;
        }

        public static Type GetExpectedRoomType(RoomType roomType)
        {
            return _roomTypeDict[roomType];
        }

        public static Type GetExpectedRoomDataType(RoomType roomType)
        {
            return _roomDataTypeDict[roomType];
        }
    }
}