using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class TilemapGenerator
    {
        private Tilemap _tilemap;
        private Tile _ground;
        private int _mapWidth;
        private int _mapHeight;
        private int _factorSmooth;
        private int _fillPercent;

        private int[,] map;
        private int countWall = 4;

        public TilemapGenerator(GeneratorLevelView levelView)
        {
            _tilemap = levelView.Tilemap;
            _ground = levelView.GroundTile;
            _mapWidth = levelView.MapWidth;
            _mapHeight = levelView.MapHeight;
            _factorSmooth = levelView.FactorSmooth;
            _fillPercent = levelView.FillPercent;
        }
    }
}
