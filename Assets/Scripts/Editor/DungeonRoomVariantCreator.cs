using System.IO;
using Data;
using Data.Constants;
using Gameplay.Dungeon;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Rewards;
using Helpers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DungeonRoomVariantCreator : EditorWindow
    {
        private RoomType _roomType;
        private int _minDepth = 0;
        private int _maxDepth = 10;
        private int _weight = 1;

        private GameObject _prefab;
        private RewardDropTable _rewardDropTable;

        private DungeonRoomsDatabase _database;

        public static void ShowWindow(RoomType roomType, DungeonRoomsDatabase database)
        {
            var window = CreateInstance<DungeonRoomVariantCreator>();
            window._roomType = roomType;
            window._database = database;
            window.titleContent = new GUIContent("Create " + roomType + " Variant");
            window.ShowUtility();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField($"Create new {_roomType} Variant", EditorStyles.boldLabel);
            _minDepth = EditorGUILayout.IntField("Min Depth", _minDepth);
            _maxDepth = EditorGUILayout.IntField("Max Depth", _maxDepth);
            _weight = EditorGUILayout.IntField("Weight", _weight);

            _prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false) as GameObject;
            _rewardDropTable =
                EditorGUILayout.ObjectField("Reward Drop Table", _rewardDropTable, typeof(RewardDropTable), false) as
                    RewardDropTable;

            if (_maxDepth < _minDepth) _maxDepth = _minDepth;

            if (GUILayout.Button("Create"))
            {
                CreateVariant();
                Close();
            }

            if (GUILayout.Button("Cancel"))
            {
                Close();
            }
        }

        private void CreateVariant()
        {
            AssetsPathsHelper.EnsureRoomCreationFolders(_roomType);

            if (_prefab == null)
            {
                Debug.LogError("A base prefab must be assigned to create a room");
                return;
            }
            
            System.Type variantType = RoomTypeHelper.GetExpectedRoomType(_roomType);
            System.Type variantDataType = RoomTypeHelper.GetExpectedRoomDataType(_roomType);

            // ----------------------------
            // 1. Create prefab variant
            // ----------------------------
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(_prefab);
            instance.name = $"{_roomType}Variant";

            // Add variant component
            if (!instance.GetComponent(variantType))
                instance.AddComponent(variantType);

            string prefabPath = AssetDatabase.GenerateUniqueAssetPath(
                AssetsPathsHelper.GetRoomPrefabPath(_roomType, instance.name)
            );

            // Save as prefab variant
            GameObject createdPrefab = PrefabUtility.SaveAsPrefabAsset(
                instance,
                prefabPath
            );

            DestroyImmediate(instance);
            
            // ----------------------------
            // 2. Create ScriptableObject data
            // ----------------------------
            string assetPath = AssetDatabase.GenerateUniqueAssetPath(
                AssetsPathsHelper.GetRoomDataPath(_roomType, $"{_roomType}VariantData")
            );

            var asset = ScriptableObject.CreateInstance(variantDataType) as RoomVariantData;

            SerializedObject so = new SerializedObject(asset);

            so.FindProperty("_minDepth").intValue = _minDepth;
            so.FindProperty("_maxDepth").intValue = _maxDepth;
            so.FindProperty("_weight").intValue = _weight;

            if (createdPrefab != null)
                so.FindProperty("_prefab").objectReferenceValue = createdPrefab;

            if (_rewardDropTable)
                so.FindProperty("_rewardDropTable").objectReferenceValue = _rewardDropTable;

            so.ApplyModifiedProperties();

            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // ----------------------------
            // 3. Register in database
            // ----------------------------
            if (_database != null)
            {
                Undo.RecordObject(_database, "Add new room variant");
                _database.Rooms.Add(asset);
                EditorUtility.SetDirty(_database);
            }

            Selection.activeObject = asset;
        }
    }
}