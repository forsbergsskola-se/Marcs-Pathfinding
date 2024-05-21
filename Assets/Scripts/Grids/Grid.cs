using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grids
{
    public class Grid : MonoBehaviour
    {
        public GridCell[] gridCells = new GridCell[100];
        public int width = 10;
        public int height = 10;
    
        public GridCell GetGridCellFromIndex(Vector2Int cellPos)
        {
            return gridCells[cellPos.y * width + cellPos.x];
        }
        public Vector2Int GetCellIndexForPosition(Vector3 position)
        {
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        }
        
        public GridCell GetCellFromPosition(Vector3 position)
        {
            return GetGridCellFromIndex(GetCellIndexForPosition(position));
        }
        
        bool IsValidAndWalkable(Vector2Int index)
        {
            if (index.x < 0) return false;
            if (index.x >= width) return false;
            if (index.y < 0) return false;
            if (index.y >= height) return false;
            return isWalkable(index);
        }


        public IEnumerable<Vector2Int> GetWalkDirections()
        {
            yield return Vector2Int.up;
            yield return Vector2Int.right;
            yield return Vector2Int.down;
            yield return Vector2Int.left;
        }
        
        
        public IEnumerable<GridCell> GetWalkableNeighboursForCell(GridCell cell)
        {
            var cellIndex = GetCellIndexForPosition(cell.transform.position);

            foreach (var direction in GetWalkDirections())
            {
                if (IsValidAndWalkable(cellIndex + direction))
                    yield return GetGridCellFromIndex(cellIndex + direction);
            }
        }


        public bool isWalkable(Vector2Int gridpos)
        {
            return GetGridCellFromIndex(gridpos).isWalkable;
        }
    }
}
