using System.IO;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Data;
using Gameplay.Rewards;
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
            _rewardDropTable = EditorGUILayout.ObjectField("Reward Drop Table", _rewardDropTable, typeof(RewardDropTable), false) as RewardDropTable;
            
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
            string folderPath = $"Assets/Configs/Dungeon/RoomVariants/{_roomType}";
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                AssetDatabase.Refresh();
            }

            // Determine type of ScriptableObject to create based on RoomType
            System.Type variantType = typeof(RoomVariantData);
            switch (_roomType)
            {
                case RoomType.Corridor: variantType = typeof(CorridorRoomVariantData); break;
                case RoomType.Decision: variantType = typeof(DecisionRoomVariantData); break;
                case RoomType.TreasureChest: variantType = typeof(TreasureChestRoomVariantData); break;
                case RoomType.Combat: variantType = typeof(CombatRoomVariantData); break;
                case RoomType.Shop: variantType = typeof(ShopRoomVariantData); break;
                case RoomType.Shrine: variantType = typeof(ShrineRoomVariantData); break;
                case RoomType.PhysicalMaster: variantType = typeof(PhysicalMasterRoomVariantData); break;
                case RoomType.MagicMaster: variantType = typeof(MagicMasterRoomVariantData); break;
                case RoomType.Trap: variantType = typeof(TrapRoomVariantData); break;
            }

            string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{_roomType}Variant.asset");
            var asset = ScriptableObject.CreateInstance(variantType) as RoomVariantData;

            // Set depth
            SerializedObject so = new SerializedObject(asset);
            
            SerializedProperty prefabProp = so.FindProperty("_prefab");
            SerializedProperty minProp = so.FindProperty("_minDepth");
            SerializedProperty maxProp = so.FindProperty("_maxDepth");
            SerializedProperty weightProp = so.FindProperty("_weight");
            SerializedProperty dropTableProp = so.FindProperty("_rewardDropTable");

            minProp.intValue = _minDepth;
            maxProp.intValue = _maxDepth;
            weightProp.intValue = _weight;
            
            if(_prefab)
                prefabProp.objectReferenceValue = _prefab;
            
            if(_rewardDropTable)
                dropTableProp.objectReferenceValue = _rewardDropTable;

            so.ApplyModifiedProperties();

            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Add to database list
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