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

        private static readonly HashSet<RoomType> NonSelectableRoomTypes = new()
        {
            RoomType.Corridor,
            RoomType.Trap,
            RoomType.EncounterBattle,
            RoomType.Decision
        };

        public static readonly HashSet<RoomType> EncounterRoomsTypes = new()
        {
            RoomType.Trap,
            RoomType.EncounterBattle
        };

        public static bool IsRecordableRoom(RoomType roomType)
        {
            return IsRoomValidForSelection(roomType);
        }
        
        public static bool IsRoomValidForSelection(RoomType roomType)
        {
            return !NonSelectableRoomTypes.Contains(roomType);
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