using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Dungeon.Data;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingSelector
    {
        private const int RoomsForSelectionCount = 3;
        
        private readonly PlayerMovementHistory _playerMovementHistory;
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;

        public List<RoomVariantData> RoomsForSelection { get; } = new(RoomsForSelectionCount);

        public DungeonBranchingSelector(PlayerMovementHistory playerMovementHistory,
            DungeonRoomsProvider dungeonRoomsProvider)
        {
            _playerMovementHistory = playerMovementHistory;
            _dungeonRoomsProvider = dungeonRoomsProvider;
        }

        public void PrepareSelection()
        {
            RoomsForSelection.Clear();

            List<RoomVariantData> candidates = _dungeonRoomsProvider.GetRoomsSelection();
            if (candidates == null || candidates.Count < RoomsForSelectionCount)
            {
                Debug.LogWarning("DungeonBranchingSelector: no valid rooms available for selection.");
                return;
            }

            while (RoomsForSelection.Count < RoomsForSelectionCount && candidates.Count > 0)
            {
                RoomVariantData picked = WeightedPickFromList(candidates);
                if (picked == null)
                    break;

                RoomsForSelection.Add(picked);

                RemoveAllOfType(candidates, picked.RoomType);
            }
        }

        private RoomVariantData WeightedPickFromList(List<RoomVariantData> list)
        {
            int size = list.Count;
            
            if (size == 0)
                return null;

            int totalWeight = 0;
            for (int i = 0; i < size; i++)
                totalWeight += list[i].Weight;

            int roll = Random.Range(0, totalWeight);

            for (int i = 0; i < size; i++)
            {
                RoomVariantData data = list[i];
                if (roll < data.Weight)
                    return data;

                roll -= data.Weight;
            }

            return list[^1];
        }

        private void RemoveAllOfType(List<RoomVariantData> list, RoomType type)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].RoomType == type)
                    list.RemoveAt(i);
            }
        }
    }
}