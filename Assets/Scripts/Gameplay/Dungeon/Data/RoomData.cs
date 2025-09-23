using Gameplay.Dungeon.Rooms;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "Dungeon/Data/RoomData")]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private DungeonRoom _prefab;
        [SerializeField] private Texture2D _icon;
        
        public DungeonRoom RoomPrefab => _prefab;
        public Texture2D Icon => _icon;
    }
}