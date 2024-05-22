using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var path = FindPath_BreadthFirst(grid, grid.GetCellFromPosition(transform.position), grid.GetCellFromPosition(goal.position));
            
            foreach (var node in path)
                node.spriteRenderer.color = Color.green;

            StartCoroutine(Co_WalkPath(path));
        }
    }

    IEnumerator Co_WalkPath(IEnumerable<GridCell> path)
    {
        foreach (var cell in path)
        {
            while (Vector3.Distance(transform.position, cell.transform.position) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, cell.transform.position, Time.deltaTime);
                yield return null;
            }
        }
    }

    static IEnumerable<GridCell> FindPath_Depth(Grid grid, GridCell start, GridCell end)
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
                    return path.Reverse();

                foundNextNode = true;
                break;
            }

            if (!foundNextNode)
                path.Pop();
        }
        
        
        return null;
    }
    
    
    static IEnumerable<GridCell> FindPath_BreadthFirst(Grid grid, GridCell start, GridCell end)
    {
        Queue<GridCell> todo = new Queue<GridCell>();
        HashSet<GridCell> visited = new HashSet<GridCell>();
        
        todo.Enqueue(start);
        visited.Add(start);
        Dictionary<GridCell, GridCell> previous = new();

        while (todo.Count > 0)
        {
            var current = todo.Dequeue();
            foreach (var neighbour in grid.GetWalkableNeighboursForCell(current))
            {
                if(visited.Contains(neighbour)) 
                    continue;
                
                todo.Enqueue(neighbour);
                previous[neighbour] = current;
                visited.Add(neighbour);
                
                neighbour.spriteRenderer.color = Color.cyan;
                
                if (neighbour == end)
                    return BuildPath(neighbour, previous).Reverse();
                
                break;
            }

        }
        
        
        return null;
    }

    private static IEnumerable<GridCell> BuildPath(GridCell neighbour, Dictionary<GridCell, GridCell> previous)
    {
        while (true)
        {
            yield return neighbour;
            if (!previous.TryGetValue(neighbour, out neighbour))
                yield break;
        }
        
        
    }
}
