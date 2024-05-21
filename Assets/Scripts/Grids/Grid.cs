using UnityEngine;

namespace Grids
{
    public class Grid : MonoBehaviour
    {
        public GridCell[] gridCells = new GridCell[100];
        public int width = 10;

    
    
        public GridCell GetGridCell(int x, int y)
        {
            Debug.Log(x + " " + y);
            return gridCells[y * width + x];
        }

        public GridCell GetCellFromPosition(Vector3 position)
        {
            return GetGridCell(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        }
    
        public bool isWalkable(int x, int y)
        {
            return GetGridCell(x, y).isWalkable;
        }
    }
}