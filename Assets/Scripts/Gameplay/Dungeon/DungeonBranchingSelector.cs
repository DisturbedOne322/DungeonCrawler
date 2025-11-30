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
        
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;

        public List<RoomVariantData> RoomsForSelection { get; } = new(RoomsForSelectionCount);

        private readonly Dictionary<RoomType, int> _selectionHistory = new ();
        private readonly List<float> _weights = new();

        public DungeonBranchingSelector(DungeonRoomsProvider dungeonRoomsProvider)
        {
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

                RoomsForSelection.Add(picked);
                
                if(!_selectionHistory.TryAdd(picked.RoomType, 1))
                    _selectionHistory[picked.RoomType]++;
                
                RemoveAllOfType(candidates, picked.RoomType);
            }

            ResetUnusedRoomTypes();
        }

        private RoomVariantData WeightedPickFromList(List<RoomVariantData> list)
        {
            int size = list.Count;
            if (size == 0)
                return null;

            _weights.Clear();
            float totalWeight = 0f;
            
            for (int i = 0; i < size; i++)
            {
                float weight = GetWeight(list[i]);
                
                totalWeight += weight;
                _weights.Add(weight);
            }

            float roll = Random.Range(0f, totalWeight);

            for (int i = 0; i < size; i++)
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
            int picks = 0;
            _selectionHistory.TryGetValue(type, out picks);
            
            if (picks == 0)
                return data.Weight;
            
            float penalty = 1f / (1f + Mathf.Exp(-1.0f * (picks - 1 - 1f)));
            return data.Weight * (1f - penalty);
        }

        private void ResetUnusedRoomTypes()
        {
            foreach (var roomType in _selectionHistory.Keys.ToList())
            {
                bool wasSelected = false;
                
                foreach (var selectedRoom in RoomsForSelection)
                {
                    if (selectedRoom.RoomType == roomType)
                    {
                        wasSelected = true;
                        break;
                    };
                }
                
                if(!wasSelected) 
                    _selectionHistory[roomType] = 0;
            }
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