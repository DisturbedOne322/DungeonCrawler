using System.Collections.Generic;
using Gameplay.Dungeon.RoomVariants;
using Helpers;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class EncounterRoomSelector
    {
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;

        public EncounterRoomSelector(DungeonRoomsProvider dungeonRoomsProvider)
        {
            _dungeonRoomsProvider = dungeonRoomsProvider;
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
            
            int index = Random.Range(
                selected.StartOffset,
                rooms - selected.EndOffset);

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

            foreach (var roomType in RoomTypeHelper.EncounterRoomTypes)
            {
                var roomData = _dungeonRoomsProvider.GetRoomData(roomType);
                if(roomData)
                    selection.Add(roomData);
            }
            
            return selection;
        }
        
        private EncounterRoomVariantData PickByWeight(
            IReadOnlyList<RoomVariantData> rooms)
        {
            int totalWeight = 0;

            foreach (var room in rooms)
                totalWeight += room.Weight;

            int roll = Random.Range(0, totalWeight);

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