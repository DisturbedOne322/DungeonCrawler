#if UNITY_EDITOR
using System.Collections.Generic;
using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;
using UnityEditor;

public class DungeonRoomFinderWindow : EditorWindow
{
    private DungeonRoomsDatabase _database;
    private List<GameObject> _roomPrefabs = new List<GameObject>();
    private Vector2 _scrollPos;

    [MenuItem("Tools/Dungeon Room Finder")]
    private static void OpenWindow()
    {
        GetWindow<DungeonRoomFinderWindow>("Dungeon Room Finder");
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        _database = (DungeonRoomsDatabase)EditorGUILayout.ObjectField(
            "Dungeon Rooms Database",
            _database,
            typeof(DungeonRoomsDatabase),
            false);
        if (EditorGUI.EndChangeCheck())
        {
            RefreshPrefabs();
        }

        if (_database == null)
        {
            EditorGUILayout.HelpBox("Assign a DungeonRoomsDatabase to manage.", MessageType.Info);
            return;
        }

        if (_roomPrefabs.Count == 0)
        {
            EditorGUILayout.LabelField("No DungeonRoom prefabs found in project.");
            return;
        }

        // Get current list from database
        var currentList = GetRoomDataList(_database);

        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        // Table header
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prefab", GUILayout.Width(200));
        EditorGUILayout.LabelField("Room Type", GUILayout.Width(100));
        EditorGUILayout.LabelField("Add to Database", GUILayout.Width(120));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        // Table rows
        foreach (var prefab in _roomPrefabs)
        {
            if (!prefab.TryGetComponent<DungeonRoom>(out var room))
                continue;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.ObjectField(prefab, typeof(GameObject), false, GUILayout.Width(200));
            EditorGUILayout.LabelField(room.RoomType.ToString(), GUILayout.Width(100));

            bool alreadyAdded = currentList.Contains(room);
            EditorGUI.BeginDisabledGroup(alreadyAdded);
            if (GUILayout.Button("Add", GUILayout.Width(120)))
            {
                AddToDatabase(_database, prefab, room);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void RefreshPrefabs()
    {
        _roomPrefabs.Clear();

        if (_database == null)
            return;

        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null && prefab.GetComponent<DungeonRoom>() != null)
            {
                _roomPrefabs.Add(prefab);
            }
        }
    }

    private List<DungeonRoom> GetRoomDataList(DungeonRoomsDatabase database)
    {
        var field = typeof(DungeonRoomsDatabase)
            .GetField("_roomDataList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (field == null)
        {
            Debug.LogError("Failed to access _roomDataList in DungeonRoomsDatabase.");
            return new List<DungeonRoom>();
        }

        return field.GetValue(database) as List<DungeonRoom>;
    }

    private void AddToDatabase(DungeonRoomsDatabase database, GameObject prefab, DungeonRoom room)
    {
        if (database == null)
            return;

        var list = GetRoomDataList(database);
        if (list.Contains(room))
        {
            Debug.LogWarning($"{prefab.name} is already in DungeonRoomsDatabase.");
            return;
        }

        Undo.RecordObject(database, "Add DungeonRoom prefab");
        list.Add(room);
        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        Debug.Log($"Added {prefab.name} to DungeonRoomsDatabase.");
    }
}
#endif
