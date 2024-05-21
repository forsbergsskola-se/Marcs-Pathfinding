using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform goal;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Grid grid = FindObjectOfType<Grid>();
            var path = FindPath(grid, grid.GetCellFromPosition(transform.position), grid.GetCellFromPosition(goal.position));
        }
    }


    static IEnumerable<GridCell> FindPath(Grid grid, GridCell start, GridCell end)
    {

        return new GridCell[5];
    }
}
