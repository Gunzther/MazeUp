using Map;
using Map.Data;
using PathFinding.Algorithm;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class PathFinder
    {
        public enum Algorithm
        {
            AStar
        }

        private MapGenerator _mapGenerator;
        private IPathFindingAlgorithm _pathFindingAlgorithm;
        private Transform _from;
        private Transform _to;

        public PathFinder(MapGenerator mapGenerator, Algorithm algo)
        {
            _mapGenerator = mapGenerator;
            _pathFindingAlgorithm = algo switch
            {
                _ => new AStarAlgorithm(mapGenerator.BlockDataArray),
            };
        }

        public void SetTranforms(Transform from, Transform to)
        {
            _from = from;
            _to = to;
        }

        public List<BlockData> FindShortestPath()
        {
            BlockData currentData = _mapGenerator.GetBlockByPoint(_from.position.x, _from.position.z);
            BlockData targetData = _mapGenerator.GetBlockByPoint(_to.position.x, _to.position.z);
            return _pathFindingAlgorithm.FindShortestPath(currentData, targetData);
        }
    }
}