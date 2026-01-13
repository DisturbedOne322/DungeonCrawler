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
            => $"{GetRoomPrefabsFolder(roomType)}/{prefabName}.prefab";

        public static string GetRoomDataPath(RoomType roomType, string assetName)
            => $"{GetRoomVariantDataTypeFolder(roomType)}/{assetName}.asset";
        
        public static void EnsureRoomCreationFolders(RoomType roomType)
        {
            EnsureFolder(GetRoomVariantDataTypeFolder(roomType));
            EnsureFolder(GetRoomPrefabsFolder(roomType));
        }

        private static string GetRoomVariantDataTypeFolder(RoomType roomType)
            => $"{RoomVariantsDataFolder}/{roomType}";

        private static string GetRoomPrefabsFolder(RoomType roomType)
            => $"{RoomPrefabsFolder}/{roomType}";
        
        private static void EnsureFolder(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parent = System.IO.Path.GetDirectoryName(path);
                string name = System.IO.Path.GetFileName(path);
                AssetDatabase.CreateFolder(parent, name);
            }
        }
    }
}