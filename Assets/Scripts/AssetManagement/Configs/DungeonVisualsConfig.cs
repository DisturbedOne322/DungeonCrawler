using System;
using AYellowpaper.SerializedCollections;
using Data;
using Data.Constants;
using UnityEngine;

namespace AssetManagement.Configs
{
    [CreateAssetMenu(menuName = MenuPaths.VisualsDungeon + "DungeonVisualsDatabaseConfig")]
    public class DungeonVisualsConfig : BaseConfig
    {
        [SerializedDictionary("ROOM TYPE", "ROOM ICON")] [SerializeField]
        private SerializedDictionary<RoomType, Sprite> _typeToIconDict;

        public bool TryGetRoomIcon(RoomType roomType, out Sprite icon)
        {
            if (!_typeToIconDict.TryGetValue(roomType, out icon) || !icon)
                throw new Exception($"No icon registered for room type {roomType}");

            return true;
        }
    }
}