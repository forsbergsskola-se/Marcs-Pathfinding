using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid))]
public class GridInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Grid"))
        {
            Grid grid = target as Grid;
            
            EditorUtility.SetDirty(grid);
        }
    }
}
