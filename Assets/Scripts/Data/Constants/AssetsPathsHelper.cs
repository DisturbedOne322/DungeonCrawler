using System.IO;
using UnityEditor;

namespace Data.Constants
{
    public static class AssetsPathsHelper
    {
        public const string BootstrapScenePath = "Assets/Scenes/MainMenuScene.unity";
        public const string GameplayScenePath = "Assets/Scenes/GameplayScene.unity";

        private const string RoomVariantsDataFolder = "Assets/Configs/Dungeon/RoomVariants";
        private const string RoomPrefabsFolder = "Assets/Prefabs/Gameplay/Dungeon/Rooms";

        public static string GetRoomPrefabPath(RoomType roomType, string prefabName)
        {
            return $"{GetRoomPrefabsFolder(roomType)}/{prefabName}.prefab";
        }

        public static string GetRoomDataPath(RoomType roomType, string assetName)
        {
            return $"{GetRoomVariantDataTypeFolder(roomType)}/{assetName}.asset";
        }

        public static void EnsureRoomCreationFolders(RoomType roomType)
        {
            EnsureFolder(GetRoomVariantDataTypeFolder(roomType));
            EnsureFolder(GetRoomPrefabsFolder(roomType));
        }

        private static string GetRoomVariantDataTypeFolder(RoomType roomType)
        {
            return $"{RoomVariantsDataFolder}/{roomType}";
        }

        private static string GetRoomPrefabsFolder(RoomType roomType)
        {
            return $"{RoomPrefabsFolder}/{roomType}";
        }

        private static void EnsureFolder(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                var parent = Path.GetDirectoryName(path);
                var name = Path.GetFileName(path);
                AssetDatabase.CreateFolder(parent, name);
            }
        }
    }
}