using DefaultNamespace;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Grid))]
public class GridInspector : Editor
{

    private Object cellPrefab;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        cellPrefab = EditorGUILayout.ObjectField("Cell Prefab", cellPrefab, typeof(GridCell));
        GUI.enabled = cellPrefab != null;

        if (GUILayout.Button("Generate Grid"))
        {
            Grid grid = target as Grid;

            if (EditorUtility.IsDirty(grid))
            {
                foreach (var cell in FindObjectsOfType<GridCell>())
                {
                    if(cell != null)
                        DestroyImmediate(cell.gameObject);
                }
                
                
            }
            
            EditorUtility.SetDirty(grid);
            GenerateCells(grid);
        }
        GUI.enabled = true;
    }
    
    private void GenerateCells(Grid grid)
    {
        int index = 0;
        
        for(int y = 0; y < (grid.gridCells.Length) / grid.width; y++)
            for (int x = 0; x < grid.width; x++)
            {
                var cell = (GridCell)PrefabUtility.InstantiatePrefab(cellPrefab);
                cell.transform.position = new Vector3(x, y);
                cell.name = "GridCell{" + x + ", " + y + "]";
                grid.gridCells[index] = cell;
                index++;
            }
    }
}
