using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridCell : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public bool isWalkable;

        private void Start()
        {
            OnValidate();
        }

        public void OnValidate()
        {
            spriteRenderer.color = isWalkable ? Color.white : Color.black;
        }
    }
}