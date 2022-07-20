using Map.Data;
using System.Collections.Generic;

namespace PathFinding.Algorithm
{
    public interface IPathFindingAlgorithm
    {
        public List<BlockData> FindShortestPath(BlockData start, BlockData end);
    }
}