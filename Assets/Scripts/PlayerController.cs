using System.Collections;
using System.Collections.Generic;
using Grids;
using UnityEngine;
using Grid = Grids.Grid;

public class PlayerController : MonoBehaviour
{
    public Transform goal;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Grid grid = FindObjectOfType<Grid>();
            var path = FindPath(grid, grid.GetCellFromPosition(transform.position), grid.GetCellFromPosition(goal.position));
            
            foreach (var node in path)
            {
                node.spriteRenderer.color = Color.green;
            }
            
        }
    }


    static IEnumerable<GridCell> FindPath(Grid grid, GridCell start, GridCell end)
    {
        Stack<GridCell> path = new Stack<GridCell>();
        HashSet<GridCell> visited = new HashSet<GridCell>();
        
        path.Push(start);
        visited.Add(start);

        while (path.Count > 0)
        {
            bool foundNextNode = false;
            foreach (var neighbour in grid.GetWalkableNeighboursForCell(path.Peek()))
            {
                if(visited.Contains(neighbour)) 
                    continue;
                
                path.Push(neighbour);
                visited.Add(neighbour);
                neighbour.spriteRenderer.color = Color.cyan;
                
                if (neighbour == end)
                    return path;

                foundNextNode = true;
                break;
            }

            if (!foundNextNode)
                path.Pop();
        }
        
        
        return null;
    }
}
