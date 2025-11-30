using System.Collections.Generic;
using System.Linq;
using Data;
using Gameplay.Dungeon.Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DungeonRoomDatabaseVisualizeWindow : EditorWindow
    {
        private DungeonRoomsDatabase _database;

        private readonly Dictionary<RoomType, Color> _columnColors = new()
        {
            { RoomType.Corridor, new Color(0.6f, 0.6f, 0.6f) },
            { RoomType.Decision, new Color(0.8f, 0.8f, 0.4f) },
            { RoomType.TreasureChest, new Color(1f, 0.8f, 0.2f) },
            { RoomType.Combat, new Color(1f, 0.4f, 0.4f) },
            { RoomType.Shop, new Color(0.4f, 1f, 0.4f) },
            { RoomType.Shrine, new Color(0.4f, 0.8f, 1f) },
            { RoomType.PhysicalMaster, new Color(0.6f, 0.3f, 1f) },
            { RoomType.MagicMaster, new Color(1f, 0.3f, 1f) },
        };

        private Vector2 _scrollPos;

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
                return;

            var rooms = _database.Rooms;
            if (rooms == null || rooms.Count == 0)
                return;

            // Calculate overall max depth
            int overallMaxDepth = rooms.Max(r => r.MaxDepth);

            // Setup scroll view
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            float windowWidth = position.width - 20;
            float columnWidth = windowWidth / System.Enum.GetValues(typeof(RoomType)).Length;
            float graphHeight = 400f;

            Rect graphRect = GUILayoutUtility.GetRect(windowWidth, graphHeight);

            // Draw columns per RoomType
            int roomTypeIndex = 0;
            foreach (RoomType type in System.Enum.GetValues(typeof(RoomType)))
            {
                var variants = rooms.Where(r => r.RoomType == type).ToList();
                if (variants.Count == 0)
                {
                    roomTypeIndex++;
                    continue;
                }

                Rect columnRect = new Rect(
                    graphRect.x + columnWidth * roomTypeIndex,
                    graphRect.y,
                    columnWidth,
                    graphHeight
                );

                EditorGUI.DrawRect(columnRect, new Color(0.1f, 0.1f, 0.1f, 0.1f));

                // Draw each room variant as a rectangle
                foreach (var variant in variants)
                {
                    float yMin = columnRect.y + graphHeight * ((float)variant.MinDepth / overallMaxDepth);
                    float yMax = columnRect.y + graphHeight * ((float)variant.MaxDepth / overallMaxDepth);
                    float height = yMax - yMin;

                    Rect rect = new Rect(columnRect.x + 5, yMin, columnWidth - 10, height);

                    // Draw colored box
                    EditorGUI.DrawRect(rect, _columnColors[type]);

                    // Draw outline
                    Handles.color = Color.black;
                    Handles.DrawAAPolyLine(2f, new Vector3[]
                    {
                        new Vector3(rect.x, rect.y),
                        new Vector3(rect.x + rect.width, rect.y),
                        new Vector3(rect.x + rect.width, rect.y + rect.height),
                        new Vector3(rect.x, rect.y + rect.height),
                        new Vector3(rect.x, rect.y)
                    });

                    // Draw text
                    GUIStyle textStyle = new GUIStyle(EditorStyles.boldLabel)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        normal = { textColor = Color.black },
                        fontSize = 11
                    };

                    EditorGUI.LabelField(rect, $"{variant.MinDepth}-{variant.MaxDepth}", textStyle);
                }

                // Draw column label
                Rect labelRect = new Rect(columnRect.x, columnRect.y + graphHeight + 5, columnWidth, 20);
                GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter
                };
                EditorGUI.LabelField(labelRect, type.ToString(), labelStyle);

                roomTypeIndex++;
            }

            EditorGUILayout.EndScrollView();
        }
    }
}