#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGrid), true)]
public class BaseActionEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

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
    }
}
#endif