﻿using UnityEditor;
using UnityEngine;

public class SceneManagerAssetPostProcessor : UnityEditor.AssetModificationProcessor
{
    public static string[] OnWillSaveAssets(string[] paths)
    {
        foreach (string path in paths)
        {
            if (path.EndsWith(".unity"))
            {
                AssetDatabase.LoadAssetAtPath<Object>(path);
                SceneManager manager = Object.FindObjectOfType<SceneManager>();
                manager.SaveScenePrefabs();
            }
        }

        return paths;
    }
}

[InitializeOnLoad]
public class AutosaveOnRun : ScriptableObject
{
    static AutosaveOnRun()
    {
        EditorApplication.playModeStateChanged += state =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                SceneManager manager = Object.FindObjectOfType<SceneManager>();
                manager.SaveScenePrefabs();
            }
        };
    }
}

[CustomEditor(typeof(SceneManager))]
public class SceneManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Color oldColor = GUI.color;

        GUILayout.Space(10);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_outputPath"), new GUIContent("Output Path"));
        GUI.color = Color.green;
        if (GUILayout.Button("Save Scene Prefab", GUILayout.Height(50)))
        {
            SaveScenePrefabs();
        }


        GUILayout.Space(10);

        GUI.color = Color.red;
        if (GUILayout.Button("Reload Scene Prefab", GUILayout.Height(50)))
        {
            ReloadScenePrefabs();
        }

        GUI.color = oldColor;

        GUILayout.Space(10);

        serializedObject.ApplyModifiedProperties();
    }

    private void SaveScenePrefabs()
    {
        SceneManager sceneManager = target as SceneManager;
        if (sceneManager != null)
        {
            sceneManager.SaveScenePrefabs();
        }
    }

    private void ReloadScenePrefabs()
    {
        SceneManager sceneManager = target as SceneManager;
        if (sceneManager != null)
        {
            sceneManager.ReloadScenePrefabs();
        }
    }
}