using Gameplay.Dungeon.Areas;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "Dungeon/Data/RoomData")]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private DungeonRoom _prefab;
        [SerializeField] private Sprite _icon;
        
        public DungeonRoom RoomPrefab => _prefab;
        public Sprite Icon => _icon;
    }
}