using Grids;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Grid = Grids.Grid;

namespace Tests
{
    public class GridTests
    {

        private Grid grid;

        [SetUp]
        void SetUp()
        {
            var gridGameObject = new GameObject("Grid");
            grid = gridGameObject.AddComponent<Grid>();
            grid.width = 3;
            int height = 3;

            grid.gridCells = new GridCell[9];

            int i = 0;
            
            for(int y = 0; y < height; y++)
            for (int x = 0; x < grid.width; x++)
            {
                var cellGameObject = new GameObject("GridCell");
                cellGameObject.transform.SetParent(grid.transform);
                cellGameObject.transform.position = new Vector3(x, y, 0);
                cellGameObject.AddComponent<SpriteRenderer>();
                var cell = cellGameObject.AddComponent<GridCell>();
                grid.gridCells[i++] = cell;
            }
        }

        [TearDown]
        void TearDown()
        {
            Object.DestroyImmediate(grid);
            grid = null;
        }

        [NUnit.Framework.Test]
        public void GridTestsSimplePasses()
        {
            // Use the Assert class to test conditions.
            
        }
   
        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityEngine.TestTools.UnityTest]
        public System.Collections.IEnumerator GridTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}