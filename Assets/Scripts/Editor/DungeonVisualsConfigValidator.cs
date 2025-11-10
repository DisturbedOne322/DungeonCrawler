#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using AssetManagement.Configs;
using Data;
using UnityEditor;
using UnityEngine;

public class DungeonVisualsConfigValidator : EditorWindow
{
    private DungeonVisualsConfig _config;
    private Vector2 _scrollPos;
    private readonly List<string> _validationMessages = new();

    [MenuItem("Tools/Dungeon Visuals Config Validator")]
    private static void OpenWindow()
    {
        GetWindow<DungeonVisualsConfigValidator>("Dungeon Visuals Validator");
    }

    private void OnEnable()
    {
        FindConfig();
        ValidateConfig();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();
        _config = (DungeonVisualsConfig)EditorGUILayout.ObjectField(
            "Dungeon Visuals Config",
            _config,
            typeof(DungeonVisualsConfig),
            false);
        if (EditorGUI.EndChangeCheck()) ValidateConfig();

        if (_config == null)
        {
            EditorGUILayout.HelpBox("No DungeonVisualsConfig assigned or found.", MessageType.Warning);
            if (GUILayout.Button("Find Config in Project"))
            {
                FindConfig();
                ValidateConfig();
            }

            return;
        }

        if (_validationMessages.Count == 0)
        {
            EditorGUILayout.HelpBox("Validation passed! All RoomTypes have assigned icons.", MessageType.Info);
        }
        else
        {
            EditorGUILayout.HelpBox("Validation failed. See messages below.", MessageType.Error);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(300));
            foreach (var msg in _validationMessages)
                EditorGUILayout.LabelField("• " + msg, EditorStyles.wordWrappedLabel);
            EditorGUILayout.EndScrollView();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Re-Validate")) ValidateConfig();
    }

    private void FindConfig()
    {
        var guids = AssetDatabase.FindAssets("t:DungeonVisualsConfig");

        if (guids.Length == 0)
        {
            _config = null;
            Debug.LogError("No DungeonVisualsConfig found in project.");
            return;
        }

        if (guids.Length > 1)
            Debug.LogWarning("Multiple DungeonVisualsConfig assets found. Using the first one.");

        var path = AssetDatabase.GUIDToAssetPath(guids[0]);
        _config = AssetDatabase.LoadAssetAtPath<DungeonVisualsConfig>(path);
    }

    private void ValidateConfig()
    {
        _validationMessages.Clear();

        if (_config == null)
        {
            _validationMessages.Add("DungeonVisualsConfig not assigned.");
            return;
        }

        // Access private _typeToIconDict using reflection
        var field = typeof(DungeonVisualsConfig)
            .GetField("_typeToIconDict", BindingFlags.NonPublic | BindingFlags.Instance);

        if (field == null)
        {
            _validationMessages.Add("Could not find _typeToIconDict in DungeonVisualsConfig.");
            return;
        }

        var dict = field.GetValue(_config) as IDictionary<RoomType, Sprite>;

        if (dict == null)
        {
            _validationMessages.Add("_typeToIconDict is null or not a dictionary.");
            return;
        }

        // Validate all enum values
        foreach (RoomType type in Enum.GetValues(typeof(RoomType)))
            if (!dict.ContainsKey(type))
                _validationMessages.Add($"Missing entry for RoomType: {type}");
            else if (dict[type] == null) _validationMessages.Add($"RoomType {type} has a null Sprite assigned.");
    }
}
#endif