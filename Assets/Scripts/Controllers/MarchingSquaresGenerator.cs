using UnityEngine.Tilemaps;
using UnityEngine;

namespace PlatformerMVC
{
    public class MarchingSquaresGenerator
    {

        private Tilemap _tileMap;
        private Tile _tile;
        private SquareGrid _squareGrid;

        public void GeneratorInit(int[,] map, float SquareSize)
        {
            _squareGrid = new SquareGrid(map, SquareSize);
        }

        public void DrawTiles(Tilemap tileMap, Tile ground)
        {
            if (_squareGrid == null)
                return;

            _tileMap = tileMap;
            _tile = ground;

            for (int x = 0; x < _squareGrid.Squares.GetLength(0); x++)
            {
                for (int y = 0; y < _squareGrid.Squares.GetLength(1); y++)
                {
                    DrawControlNode(_squareGrid.Squares[x, y].TopLeft.Active, _squareGrid.Squares[x, y].TopLeft.Position);
                    DrawControlNode(_squareGrid.Squares[x, y].TopRight.Active, _squareGrid.Squares[x, y].TopRight.Position);
                    DrawControlNode(_squareGrid.Squares[x, y].BottomLeft.Active, _squareGrid.Squares[x, y].BottomLeft.Position);
                    DrawControlNode(_squareGrid.Squares[x, y].BottomRight.Active, _squareGrid.Squares[x, y].BottomRight.Position);
                }
            }
        }

        private void DrawControlNode(bool active, Vector3 pos)
        {
            if(active)
            {
                Vector3Int TilePos = new Vector3Int((int)pos.x, (int)pos.y, 0);
                _tileMap.SetTile(TilePos, _tile);
            }
        }
    }

    public class Node
    {
        public Vector3 Position;

        public Node(Vector3 _pos)
        {
            Position = _pos;
        }
    }

    public class ControlNode: Node
    {
        public bool Active;

        public ControlNode(Vector3 pos, bool active): base(pos)
        {
            Active = active;
        }
    }

    public class Square
    {
        public ControlNode TopLeft, TopRight, BottomLeft, BottomRight;

        public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomLeft, ControlNode bottomRight)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
        }
    }

    public class SquareGrid
    {
        public Square[,] Squares;

        public SquareGrid (int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);
            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            var controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 position = new Vector3((-mapWidth / 2) + (x * squareSize) + squareSize / 2, (-mapHeight / 2) + (y * squareSize) + squareSize / 2);
                    controlNodes[x, y] = new ControlNode(position, map[x, y] == 1);
                }
            }

            Squares = new Square [nodeCountX - 1, nodeCountY - 1];

            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    Squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }
            }
        }
    }
}
