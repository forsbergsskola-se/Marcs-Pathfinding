using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Collections;
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
            var path = FindPath_Dijkstra(grid, grid.GetCellFromPosition(transform.position), grid.GetCellFromPosition(goal.position));
            
            foreach (var node in path)
                node.spriteRenderer.ShiftBrightness(0.4f);

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
                    return TracePath(neighbour, previous).Reverse();
            }

        }
        
        
        return null;
    }

    static IEnumerable<GridCell> FindPath_Dijkstra(Grid grid, GridCell start, GridCell end)
    {
        PriorityQueue<GridCell> todo = new();
        HashSet<GridCell> visited = new();
        
        todo.Enqueue(start, 0);
        visited.Add(start);
        Dictionary<GridCell, int> costs = new();
        costs[start] = 0;
        Dictionary<GridCell, GridCell> previous = new();
        
        while (todo.Count > 0)
        {
            var current = todo.Dequeue();
            if (current == end)
                return TracePath(current, previous).Reverse();
            
            foreach (var neighbour in grid.GetWalkableNeighboursForCell(current))
            {
                int newNeighbourCost = costs[current] + neighbour.cost;
                if(costs.TryGetValue(neighbour, out int neighbourCost) && 
                   neighbourCost <= newNeighbourCost) 
                    continue;
                
                todo.Enqueue(neighbour, newNeighbourCost);
                previous[neighbour] = current;
                costs[neighbour] = newNeighbourCost;
                
                neighbour.spriteRenderer.ShiftBrightness(0.4f);
            }

        }
        
        
        return null;
    }
    private static IEnumerable<GridCell> TracePath(GridCell neighbour, Dictionary<GridCell, GridCell> previous)
    {
        while (true)
        {
            yield return neighbour;
            if (!previous.TryGetValue(neighbour, out neighbour))
                yield break;
        }
        
        
    }
    
    
}

public static class SpriteRendererExtensions
{
    public static void ShiftHue(this SpriteRenderer spriteRenderer, float hue)
    {
        Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
        spriteRenderer.color = Color.HSVToRGB((h + hue)%1, s, v);
    }
    
    public static void ShiftBrightness(this SpriteRenderer spriteRenderer, float brightness)
    {
        Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
        if (v > 0.5f)
        {
            spriteRenderer.color = Color.HSVToRGB(h, s, (v-brightness)%1f);
        }
        else
        {
            spriteRenderer.color = Color.HSVToRGB(h, s, (v+brightness)%1f);
        }
    }
}