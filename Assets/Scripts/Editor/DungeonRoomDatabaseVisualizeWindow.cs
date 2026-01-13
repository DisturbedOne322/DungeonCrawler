using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.RoomVariants;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DungeonRoomDatabaseVisualizeWindow : EditorWindow
    {
        private DungeonRoomsDatabase _database;
        private Vector2 _scrollPos;

        private readonly Dictionary<RoomType, Color> _columnColors = new()
        {
            { RoomType.Corridor, new Color(0.6f, 0.6f, 0.6f) },
            { RoomType.Decision, new Color(0.8f, 0.8f, 0.4f) },
            { RoomType.TreasureChest, new Color(1f, 0.8f, 0.2f) },
            { RoomType.BasicFight, new Color(1f, 1, 0.4f) },
            { RoomType.EliteFight, new Color(1f, 0.8f, 0.4f) },
            { RoomType.BossFight, new Color(1f, 0.4f, 0.4f) },
            { RoomType.Shop, new Color(0.4f, 1f, 0.4f) },
            { RoomType.Shrine, new Color(0.4f, 0.8f, 1f) },
            { RoomType.PhysicalMaster, new Color(0.6f, 0.3f, 1f) },
            { RoomType.MagicMaster, new Color(1f, 0.3f, 1f) },
            { RoomType.Trap, new Color(0.8f, 0.8f, 0.4f) },
            { RoomType.RandomEncounter, new Color(0.65f, 1f, 0.5f) },
        };

        [MenuItem("Tools/Dungeon Rooms Visualizer")]
        public static void ShowWindow()
        {
            GetWindow<DungeonRoomDatabaseVisualizeWindow>("Dungeon Rooms Visualizer");
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();

            _database = (DungeonRoomsDatabase)EditorGUILayout.ObjectField("Dungeon Database", _database,
                typeof(DungeonRoomsDatabase), false);

            if (_database == null)
            {
                FindConfig();
                
                if (_database == null)
                    return;
            }

            var rooms = _database.Rooms;
            if (rooms == null) rooms = new List<RoomVariantData>();

            // Calculate overall max depth
            int overallMaxDepth = rooms.Count > 0 ? rooms.Max(r => r.MaxDepth) : 10;

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            float windowWidth = position.width - 20;
            float columnWidth = windowWidth / System.Enum.GetValues(typeof(RoomType)).Length;
            
            float availableHeight = position.height - 100f;  
            if (availableHeight < 100f) 
                availableHeight = 100f;

            float graphHeight = availableHeight * 0.9f;

            Rect graphRect = GUILayoutUtility.GetRect(windowWidth, graphHeight);

            int roomTypeIndex = 0;
            foreach (RoomType type in System.Enum.GetValues(typeof(RoomType)))
            {
                var variants = rooms.Where(r => r.RoomType == type).ToList();

                Rect columnRect = new Rect(
                    graphRect.x + columnWidth * roomTypeIndex,
                    graphRect.y,
                    columnWidth,
                    graphHeight
                );

                EditorGUI.DrawRect(columnRect, new Color(0.1f, 0.1f, 0.1f, 0.1f));

                // Draw each room variant
                foreach (var variant in variants)
                {
                    float yMin = columnRect.y + graphHeight * ((float)variant.MinDepth / overallMaxDepth);
                    float yMax = columnRect.y + graphHeight * ((float)variant.MaxDepth / overallMaxDepth);
                    float height = yMax - yMin;

                    Rect rect = new Rect(columnRect.x + 5, yMin, columnWidth - 10, height);

                    EditorGUI.DrawRect(rect, _columnColors[type]);

                    // Outline
                    Handles.color = Color.black;
                    Handles.DrawAAPolyLine(2f, new Vector3[]
                    {
                        new (rect.x, rect.y),
                        new (rect.x + rect.width, rect.y),
                        new (rect.x + rect.width, rect.y + rect.height),
                        new (rect.x, rect.y + rect.height),
                        new (rect.x, rect.y)
                    });

                    GUIStyle textStyle = new GUIStyle(EditorStyles.boldLabel)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        normal = { textColor = Color.black },
                        fontSize = 11
                    };
                    
                    float labelHeight = 14f;
                    float spacing = 6f;
                    float totalLabelsH = labelHeight * 2 + spacing;

                    if (rect.height < totalLabelsH)
                    {
                        EditorGUI.LabelField(rect, $"D:{variant.MinDepth}-{variant.MaxDepth}  W:{variant.Weight}", textStyle);
                    }
                    else
                    {
                        float startY = rect.y + (rect.height - totalLabelsH) * 0.5f;
                        Rect depthRect = new Rect(rect.x + 2f, startY, rect.width - 4f, labelHeight);
                        Rect weightRect = new Rect(rect.x + 2f, startY + labelHeight + spacing, rect.width - 4f, labelHeight);

                        EditorGUI.LabelField(depthRect, $"Depth: {variant.MinDepth}-{variant.MaxDepth}", textStyle);
                        EditorGUI.LabelField(weightRect, $"Weight: {variant.Weight}", textStyle);
                    }

                    if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                    {
                        Selection.activeObject = variant;
                        Event.current.Use();
                    }
                }

                // Column label
                Rect labelRect = new Rect(columnRect.x, columnRect.y + graphHeight + 5, columnWidth, 20);
                GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel) { alignment = TextAnchor.MiddleCenter };
                EditorGUI.LabelField(labelRect, type.ToString(), labelStyle);

                Rect buttonRect = new Rect(columnRect.x, columnRect.y + graphHeight + 25, columnWidth, 20);
                if (GUI.Button(buttonRect, "Create")) 
                    DungeonRoomVariantCreator.ShowWindow(type, _database);

                roomTypeIndex++;
            }

            EditorGUILayout.EndScrollView();
        }
        
        private void FindConfig()
        {
            var guids = AssetDatabase.FindAssets("t:DungeonRoomsDatabase");

            if (guids.Length == 0)
            {
                _database = null;
                Debug.LogError("No DungeonRoomsDatabase found in project.");
                return;
            }

            if (guids.Length > 1)
                Debug.LogWarning("Multiple DungeonRoomsDatabase assets found. Using the first one.");

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            _database = AssetDatabase.LoadAssetAtPath<DungeonRoomsDatabase>(path);
        }
    }
}