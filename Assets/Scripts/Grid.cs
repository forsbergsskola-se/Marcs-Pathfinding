using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;


public class Grid : MonoBehaviour
{
    public GridCell[] gridCells = new GridCell[100];
    public int width = 10;

    public GridCell GetGridCell(int x, int y)
    {
        return gridCells[y * width + x];
    }

    public bool isWalkable(int x, int y)
    {
        return GetGridCell(x, y).isWalkable;
    }
}
