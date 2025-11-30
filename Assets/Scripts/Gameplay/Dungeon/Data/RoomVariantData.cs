using System;
using Data;
using Gameplay.Dungeon.RoomTypes;
using Gameplay.Rewards;
using Helpers;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public abstract class RoomVariantData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField, Min(0)] private int _minDepth = 0;
        [SerializeField] private int _maxDepth = 10;
        [SerializeField] private RewardDropTable _rewardDropTable;
        
        public abstract RoomType RoomType { get; }
        
        public GameObject Prefab => _prefab;
        public int MinDepth => _minDepth;
        public int MaxDepth => _maxDepth;
        public RewardDropTable RewardDropTable => _rewardDropTable;

        public abstract void ApplyToRoom(DungeonRoom room);
        
        private void OnValidate()
        {
            if(_minDepth > _maxDepth)
                _maxDepth = _minDepth;
            
            ValidatePrefab(_prefab);
        }

        private void ValidatePrefab(GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogError($"Prefab is missing on {name}");
                return;
            }

            var expectedType = RoomTypeHelper.GetExpectedType(RoomType);
            
            if (!prefab.TryGetComponent(expectedType, out Component c))
            {
                Debug.LogError(
                    $"Prefab '{prefab.name}' type mismatch on '{name}'. Expected component '{expectedType.Name}'."
                );
            }
        }
    }
}