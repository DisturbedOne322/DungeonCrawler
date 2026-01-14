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

        // ---------------- ZOOM ----------------
        private float _verticalZoom = 1f;
        private float _horizontalZoom = 1f;

        private const float MinZoom = 0.4f;
        private const float MaxZoom = 3.5f;
        private const float ZoomSpeed = 0.1f;

        private readonly Dictionary<RoomType, Color> _columnColors = new()
        {
            { RoomType.Corridor, new Color(0.6f, 0.6f, 0.6f) },
            { RoomType.Decision, new Color(0.8f, 0.8f, 0.4f) },
            { RoomType.TreasureChest, new Color(1f, 0.8f, 0.2f) },
            { RoomType.BasicFight, new Color(1f, 1f, 0.4f) },
            { RoomType.EliteFight, new Color(1f, 0.8f, 0.4f) },
            { RoomType.BossFight, new Color(1f, 0.4f, 0.4f) },
            { RoomType.Shop, new Color(0.4f, 1f, 0.4f) },
            { RoomType.Shrine, new Color(0.4f, 0.8f, 1f) },
            { RoomType.PhysicalMaster, new Color(0.6f, 0.3f, 1f) },
            { RoomType.MagicMaster, new Color(1f, 0.3f, 1f) },
            { RoomType.Trap, new Color(0.8f, 0.8f, 0.4f) },
            { RoomType.EncounterBattle, new Color(0.65f, 1f, 0.5f) },
        };

        [MenuItem("Tools/Dungeon Rooms Visualizer")]
        public static void ShowWindow()
        {
            GetWindow<DungeonRoomDatabaseVisualizeWindow>("Dungeon Rooms Visualizer");
        }

        private void OnGUI()
        {
            HandleZoomEvents();

            EditorGUILayout.Space();

            _database = (DungeonRoomsDatabase)EditorGUILayout.ObjectField(
                "Dungeon Database",
                _database,
                typeof(DungeonRoomsDatabase),
                false
            );

            if (_database == null)
            {
                FindConfig();
                if (_database == null)
                    return;
            }

            var rooms = _database.Rooms ?? new List<RoomVariantData>();
            int overallMaxDepth = rooms.Count > 0 ? rooms.Max(r => r.MaxDepth) : 10;

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            int columnCount = System.Enum.GetValues(typeof(RoomType)).Length;
            float baseColumnWidth = 150f;
            float columnWidth = baseColumnWidth * _horizontalZoom;

            float graphHeight = Mathf.Max(position.height * 0.75f, 200f) * _verticalZoom;
            float totalWidth = columnWidth * columnCount;

            Rect graphRect = GUILayoutUtility.GetRect(totalWidth, graphHeight);

            int columnIndex = 0;
            foreach (RoomType type in System.Enum.GetValues(typeof(RoomType)))
            {
                Rect columnRect = new Rect(
                    graphRect.x + columnWidth * columnIndex,
                    graphRect.y,
                    columnWidth,
                    graphHeight
                );

                EditorGUI.DrawRect(columnRect, new Color(0, 0, 0, 0.08f));

                var variants = rooms.Where(r => r.RoomType == type).ToList();
                var lanes = BuildLanes(variants);

                DrawLanes(columnRect, graphHeight, overallMaxDepth, lanes, _columnColors[type]);
                DrawColumnFooter(columnRect, graphHeight, type);

                columnIndex++;
            }

            EditorGUILayout.EndScrollView();
            DrawZoomHint();
        }

        // ---------------- ZOOM HANDLING ----------------

        private void HandleZoomEvents()
        {
            Event e = Event.current;
            if (e.type != EventType.ScrollWheel)
                return;

            float delta = -e.delta.y * ZoomSpeed;

            if (e.control)
                _horizontalZoom = Mathf.Clamp(_horizontalZoom + delta, MinZoom, MaxZoom);
            else
                _verticalZoom = Mathf.Clamp(_verticalZoom + delta, MinZoom, MaxZoom);

            e.Use();
            Repaint();
        }

        private void DrawZoomHint()
        {
            GUILayout.Space(4);
            EditorGUILayout.HelpBox(
                "Mouse Wheel = Vertical Zoom | Ctrl + Wheel = Horizontal Zoom",
                MessageType.Info
            );
        }

        // ---------------- LANE LOGIC ----------------

        private static List<List<RoomVariantData>> BuildLanes(List<RoomVariantData> variants)
        {
            var lanes = new List<List<RoomVariantData>>();
            var sorted = variants.OrderBy(v => v.MinDepth).ThenBy(v => v.MaxDepth);

            foreach (var v in sorted)
            {
                bool placed = false;

                foreach (var lane in lanes)
                {
                    if (!lane.Any(o => Overlaps(o, v)))
                    {
                        lane.Add(v);
                        placed = true;
                        break;
                    }
                }

                if (!placed)
                    lanes.Add(new List<RoomVariantData> { v });
            }

            return lanes;
        }

        private static bool Overlaps(RoomVariantData a, RoomVariantData b)
        {
            return a.MinDepth < b.MaxDepth && b.MinDepth < a.MaxDepth;
        }

        // ---------------- DRAWING ----------------

        private void DrawLanes(
            Rect columnRect,
            float graphHeight,
            int maxDepth,
            List<List<RoomVariantData>> lanes,
            Color color
        )
        {
            if (lanes.Count == 0) return;

            float padding = 4f;
            float laneWidth = (columnRect.width - padding * (lanes.Count + 1)) / lanes.Count;

            for (int i = 0; i < lanes.Count; i++)
            {
                float x = columnRect.x + padding + i * (laneWidth + padding);

                foreach (var v in lanes[i])
                {
                    float yMin = columnRect.y + graphHeight * ((float)v.MinDepth / maxDepth);
                    float yMax = columnRect.y + graphHeight * ((float)v.MaxDepth / maxDepth);

                    Rect rect = new Rect(
                        x,
                        yMin,
                        laneWidth,
                        Mathf.Max(yMax - yMin, 6f)
                    );

                    EditorGUI.DrawRect(rect, color);
                    DrawOutline(rect);
                    DrawLabels(rect, v);

                    if (Event.current.type == EventType.MouseDown &&
                        rect.Contains(Event.current.mousePosition))
                    {
                        Selection.activeObject = v;
                        Event.current.Use();
                    }
                }
            }
        }

        private static void DrawOutline(Rect rect)
        {
            Handles.color = Color.black;
            Handles.DrawAAPolyLine(2f, new Vector3[]
            {
                new(rect.x, rect.y),
                new(rect.x + rect.width, rect.y),
                new(rect.x + rect.width, rect.y + rect.height),
                new(rect.x, rect.y + rect.height),
                new(rect.x, rect.y)
            });
        }

        private static void DrawLabels(Rect rect, RoomVariantData v)
        {
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 11,
                normal = { textColor = Color.black }
            };

            if (rect.height < 30f)
            {
                EditorGUI.LabelField(rect, $"D:{v.MinDepth}-{v.MaxDepth}", style);
                return;
            }

            EditorGUI.LabelField(
                new Rect(rect.x, rect.y + rect.height * 0.25f, rect.width, 14),
                $"Depth: {v.MinDepth}-{v.MaxDepth}",
                style
            );

            EditorGUI.LabelField(
                new Rect(rect.x, rect.y + rect.height * 0.55f, rect.width, 14),
                $"Weight: {v.Weight}",
                style
            );
        }

        private void DrawColumnFooter(Rect columnRect, float graphHeight, RoomType type)
        {
            Rect labelRect = new Rect(columnRect.x, columnRect.y + graphHeight + 5, columnRect.width, 18);
            EditorGUI.LabelField(labelRect, type.ToString(), EditorStyles.boldLabel);

            Rect buttonRect = new Rect(columnRect.x, columnRect.y + graphHeight + 25, columnRect.width, 20);
            if (GUI.Button(buttonRect, "Create"))
                DungeonRoomVariantCreator.ShowWindow(type, _database);
        }

        // ---------------- DATABASE ----------------

        private void FindConfig()
        {
            var guids = AssetDatabase.FindAssets("t:DungeonRoomsDatabase");
            if (guids.Length == 0) return;

            _database = AssetDatabase.LoadAssetAtPath<DungeonRoomsDatabase>(
                AssetDatabase.GUIDToAssetPath(guids[0])
            );
        }
    }
}