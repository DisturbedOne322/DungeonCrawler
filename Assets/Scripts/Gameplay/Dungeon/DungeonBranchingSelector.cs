using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Constants;
using Gameplay.Dungeon.RoomVariants;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingSelector
    {
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;

        private readonly Dictionary<RoomType, int> _selectionHistory = new();
        private readonly List<float> _weights = new();

        public DungeonBranchingSelector(DungeonRoomsProvider dungeonRoomsProvider)
        {
            _dungeonRoomsProvider = dungeonRoomsProvider;
        }

        public List<RoomVariantData> RoomsForSelection { get; } = new(GameplayConstants.RoomsForSelectionMax);

        public void PrepareFirstSelection()
        {
            int selectionCount = GameplayConstants.RoomsForSelectionMax;
            SelectRooms(selectionCount);
        }

        public void PrepareSelection()
        {
            int selectionCount = ChooseSelectionCount();
            SelectRooms(selectionCount);
        }

        private void SelectRooms(int selectionCount)
        {
            Debug.Log(selectionCount);
            
            RoomsForSelection.Clear();
            var candidates = _dungeonRoomsProvider.GetRoomsSelection();
            if (candidates == null || candidates.Count < selectionCount)
            {
                Debug.LogWarning("DungeonBranchingSelector: no valid rooms available for selection.");
                return;
            }

            while (RoomsForSelection.Count < selectionCount && candidates.Count > 0)
            {
                var picked = WeightedPickFromList(candidates);

                RoomsForSelection.Add(picked);

                if (!_selectionHistory.TryAdd(picked.RoomType, 1))
                    _selectionHistory[picked.RoomType]++;

                RemoveAllOfType(candidates, picked.RoomType);
            }

            ResetUnusedRoomTypes();
        }
        
        private int ChooseSelectionCount()
        {
            return Random.Range(GameplayConstants.RoomsForSelectionMin, 
                GameplayConstants.RoomsForSelectionMax + 1);
        }
        
        private RoomVariantData WeightedPickFromList(List<RoomVariantData> list)
        {
            var size = list.Count;
            if (size == 0)
                return null;

            _weights.Clear();
            var totalWeight = 0f;

            for (var i = 0; i < size; i++)
            {
                var weight = GetWeight(list[i]);

                totalWeight += weight;
                _weights.Add(weight);
            }

            var roll = Random.Range(0f, totalWeight);

            for (var i = 0; i < size; i++)
            {
                if (roll < _weights[i])
                    return list[i];

                roll -= _weights[i];
            }

            return list[^1];
        }

        private float GetWeight(RoomVariantData data)
        {
            var type = data.RoomType;
            var picks = 0;
            _selectionHistory.TryGetValue(type, out picks);

            if (picks == 0)
                return data.Weight;

            var penalty = 1f / (1f + Mathf.Exp(-1.0f * (picks - 1 - 1f)));
            return data.Weight * (1f - penalty);
        }

        private void ResetUnusedRoomTypes()
        {
            foreach (var roomType in _selectionHistory.Keys.ToList())
            {
                var wasSelected = false;

                foreach (var selectedRoom in RoomsForSelection)
                {
                    if (selectedRoom.RoomType == roomType)
                    {
                        wasSelected = true;
                        break;
                    }

                    ;
                }

                if (!wasSelected)
                    _selectionHistory[roomType] = 0;
            }
        }

        private void RemoveAllOfType(List<RoomVariantData> list, RoomType type)
        {
            for (var i = list.Count - 1; i >= 0; i--)
                if (list[i].RoomType == type)
                    list.RemoveAt(i);
        }
    }
}