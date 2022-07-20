using Map.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding.Algorithm
{
    public class AStarAlgorithm : IPathFindingAlgorithm
    {
        private const int _startIndex = 0;

        private BlockData[,] _map;
        private int _width;
        private int _height;

        private CellData[,] _tempGrid;
        private List<CellData> _openList;
        private HashSet<CellData> _closeList;
        private HashSet<CellData> _neighbours;

        public AStarAlgorithm(BlockData[,] map)
        {
            _openList = new List<CellData>();
            _closeList = new HashSet<CellData>();
            _neighbours = new HashSet<CellData>();
            SetMap(map);
        }

        public void SetMap(BlockData[,] map)
        {
            _map = map;
            _width = _map.GetLength(0);
            _height = _map.GetLength(1);
            _tempGrid = new CellData[_width, _height];
            GenerateTempGrid();
        }

        public List<BlockData> FindShortestPath(BlockData start, BlockData end)
        {
            ResetAllCellData();
            _openList.Clear();
            _closeList.Clear();
            var startCell = _tempGrid[start.Index.x, start.Index.z];
            var endCell = _tempGrid[end.Index.x, end.Index.z];
            _openList.Add(startCell);

            while (_openList.Count > 0)
            {
                var currentIndex = 0;

                for (int i = 0; i < _openList.Count; i++)
                {
                    if (_openList[i].F < _openList[currentIndex].F)
                    {
                        currentIndex = i;
                    }
                }

                var currentCell = _openList[currentIndex];

                if (currentCell == endCell)
                {
                    return GetPath(currentCell, endCell);
                }

                _openList.Remove(currentCell);
                _closeList.Add(currentCell);
                _neighbours.Clear();
                UpdateNeighbours(currentCell.BlockData);

                foreach (var nb in _neighbours)
                {
                    if (!_closeList.Contains(nb))
                    {
                        var tempG = currentCell.G + 1;

                        if (_openList.Contains(nb) && tempG < nb.G)
                        {
                            nb.G = tempG;
                        }
                        else
                        {
                            nb.G = tempG;
                            nb.H = GetDistance(nb.BlockData.Position, end.Position);
                            _openList.Add(nb);
                        }

                        nb.CameFrom = currentCell;
                    }
                }
            }

            // do not have solution
            Debug.LogError($"[{nameof(FindShortestPath)}] there is no available path from {start.Position} to {end.Position}");
            return null;
        }

        private void GenerateTempGrid()
        {
            foreach (var data in _map)
            {
                _tempGrid[data.Index.x, data.Index.z] = new CellData(data);
            }
        }

        private void ResetAllCellData()
        {
            foreach (var data in _tempGrid)
            {
                data.Reset();
            }
        }

        private List<BlockData> GetPath(CellData lastCell, CellData endCell)
        {
            List<BlockData> path = new List<BlockData>();
            path.Add(endCell.BlockData);
            var temp = lastCell;

            while(temp.CameFrom != null)
            {
                path.Add(temp.CameFrom.BlockData);
                temp = temp.CameFrom;
            }

            path.Reverse();

            return path;
        }

        private float GetDistance((float x, float z) from, (float x, float z) to)
        {
            return (float)Math.Sqrt((Math.Pow(from.x - to.x, 2) + Math.Pow(from.z - to.z, 2)));
        }

        private void UpdateNeighbours(BlockData currentBlock)
        {
            if (!currentBlock.WallActiveSides[WallSide.TOP])
            {
                var topNb = _tempGrid[currentBlock.Index.x - 1, currentBlock.Index.z];
                _neighbours.Add(topNb);
            }
            if (!currentBlock.WallActiveSides[WallSide.BOTTOM])
            {
                var bottomNb = _tempGrid[currentBlock.Index.x + 1, currentBlock.Index.z];
                _neighbours.Add(bottomNb);
            }
            if (!currentBlock.WallActiveSides[WallSide.LEFT])
            {
                var leftNb = _tempGrid[currentBlock.Index.x, currentBlock.Index.z - 1];
                _neighbours.Add(leftNb);
            }
            if (!currentBlock.WallActiveSides[WallSide.RIGHT])
            {
                var rightNb = _tempGrid[currentBlock.Index.x, currentBlock.Index.z + 1];
                _neighbours.Add(rightNb);
            }
        }

        protected class CellData
        {
            public float F => G + H;
            public float G = 0;
            public float H = 0;
            public CellData CameFrom = null;

            public readonly BlockData BlockData;

            public CellData(BlockData data)
            {
                BlockData = data;
            }

            public void Reset()
            {
                G = 0;
                H = 0;
                CameFrom = null;
            }
        }
    }
}