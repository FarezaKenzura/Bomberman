#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(LevelGrid), true)]
public class BaseActionEditor : Editor 
{
    SerializedProperty[] properties;
    bool showPrefabs, showGridSize, showItem = false;

    void OnEnable() 
    {
        properties = new SerializedProperty[] {
            serializedObject.FindProperty("groundPrefab"),
            serializedObject.FindProperty("groundShadowPrefab"),
            serializedObject.FindProperty("blockPrefab"),
            serializedObject.FindProperty("brickPrefab"),
            serializedObject.FindProperty("gridContainer"),
            serializedObject.FindProperty("brickContainer"),
            serializedObject.FindProperty("gridWidth"),
            serializedObject.FindProperty("gridHeight"),
            serializedObject.FindProperty("gridCellSize"),
            serializedObject.FindProperty("blastRadiusPrefab"),
            serializedObject.FindProperty("extraBombPrefab"),
            serializedObject.FindProperty("speedIncreasePrefabs")  
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawFoldout(ref showPrefabs, "Prefabs", properties.Skip(0).Take(6).ToArray());
        DrawFoldout(ref showGridSize, "GridSize", properties.Skip(6).Take(3).ToArray());
        DrawFoldout(ref showItem, "Item", properties.Skip(9).Take(3).ToArray());

        LevelGrid LevelGrid = (LevelGrid)target;
        if(GUILayout.Button("Spawn Grid"))
        {
            LevelGrid.RemoveAllGrid();
            LevelGrid.SpawnGrid();
            LevelGrid.SpawnRandomBrick();
        }
        if(GUILayout.Button("Remove Grid"))
        {
            LevelGrid.RemoveAllGrid();
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DrawFoldout(ref bool show, string label, params SerializedProperty[] properties)
    {
        show = EditorGUILayout.BeginFoldoutHeaderGroup(show, label);
        if (show)
        {
            foreach (SerializedProperty property in properties)
            {
                EditorGUILayout.PropertyField(property);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
#endif