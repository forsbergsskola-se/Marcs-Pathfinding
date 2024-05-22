using System;
using UnityEngine;

namespace Grids
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridCell : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        [SerializeField] private CellType cellType;
        public enum CellType
        {
            Ground,
            Wall,
            Water
        }

        public bool isWalkable => cellType != CellType.Wall;

        public int Cost =>
                cellType switch
                {
                    CellType.Ground => 1,
                    CellType.Wall => int.MaxValue,
                    CellType.Water => 2,
                    _ => throw new ArgumentOutOfRangeException()
                };
        
        
        private void Start()
        {
            OnValidate();
        }

        public override string ToString()
        {
            int xPos = Mathf.RoundToInt(transform.position.x);
            int yPos = Mathf.RoundToInt(transform.position.y);
            
            return
                $"{nameof(GridCell)} ({xPos}|{yPos}";
        }

        public void OnValidate()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = isWalkable ? Color.white : Color.black;
        }
    }
}