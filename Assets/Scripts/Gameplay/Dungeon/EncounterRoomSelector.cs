using System.Collections.Generic;
using AssetManagement.AssetProviders.ConfigProviders;
using Gameplay.Dungeon.RoomVariants;
using Helpers;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class EncounterRoomSelector
    {
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;
        
        private DungeonRulesConfig _dungeonRulesConfig;

        public EncounterRoomSelector(DungeonRoomsProvider dungeonRoomsProvider, GameplayConfigsProvider gameplayConfigsProvider)
        {
            _dungeonRoomsProvider = dungeonRoomsProvider;
            
            _dungeonRulesConfig = gameplayConfigsProvider.GetConfig<DungeonRulesConfig>();
        }

        public EncounterRoomSpawnData? SelectSpecialRoomData(int rooms)
        {
            var candidates = GetSpecialRoomsSelection();
            if (candidates.Count == 0)
                return null;

            var selected = PickByWeight(candidates);
            if (!selected)
                return null;

            if (Random.value > selected.RollChance)
                return null;

            var index = Random.Range(
                _dungeonRulesConfig.StartEncounterOffset,
                rooms - _dungeonRulesConfig.EndEncounterOffset);

            index = Mathf.Clamp(index, 0, rooms - 1);

            return new EncounterRoomSpawnData
            {
                RoomData = selected,
                Index = index
            };
        }

        private List<RoomVariantData> GetSpecialRoomsSelection()
        {
            List<RoomVariantData> selection = new();

            foreach (var roomType in RoomTypeHelper.EncounterRoomsTypes)
            {
                var roomData = _dungeonRoomsProvider.GetRoomData(roomType);
                if (roomData)
                    selection.Add(roomData);
            }

            return selection;
        }

        private EncounterRoomVariantData PickByWeight(
            IReadOnlyList<RoomVariantData> rooms)
        {
            var totalWeight = 0;

            foreach (var room in rooms)
                totalWeight += room.Weight;

            var roll = Random.Range(0, totalWeight);

            foreach (var room in rooms)
            {
                roll -= room.Weight;
                if (roll < 0)
                    return room as EncounterRoomVariantData;
            }

            return null;
        }
    }
}