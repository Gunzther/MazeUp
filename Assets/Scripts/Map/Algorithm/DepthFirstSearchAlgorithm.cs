using Map.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Algorithm
{
    public class DepthFirstSearchAlgorithm : IMapAlgorithm
    {
        private const int _startIndex = 0;
        private const int _randomRoutePossibility = 4; // 1/4

        private List<BlockData> _neighbours;
        private BlockData[,] _blockDataArray;
        private Stack<BlockData> _blockDataStack;
        private int _width;
        private int _height;
        private int _visitCount;

        public DepthFirstSearchAlgorithm(BlockData[,] blockDataArray)
        {
            SetValues(blockDataArray);
            _neighbours = new List<BlockData>();
            _blockDataStack = new Stack<BlockData>();
        }

        public void SetValues(BlockData[,] blockDataArray)
        {
            _blockDataArray = blockDataArray;
            _width = _blockDataArray.GetLength(0);
            _height = _blockDataArray.GetLength(1);
        }

        public BlockData[,] CalculatePath()
        {
            _visitCount = 1; // reset count
            _blockDataStack.Clear();
            CalculatePath(_blockDataArray[_startIndex, _startIndex]);
            return _blockDataArray;
        }

        private void CalculatePath(BlockData currentBlock)
        {
            _neighbours.Clear();
            currentBlock.SetIsVisit(true);
            UpdateNeighbours(currentBlock);

            if (_neighbours.Count > 0)
            {
                int index = Random.Range(0, _neighbours.Count);
                var choosen = _neighbours[index];
                _blockDataStack.Push(choosen);
                UpdateWallSideActive(currentBlock, choosen);
                IncreaseEscapeRoute(currentBlock);

                _visitCount++;
                if (_visitCount >= _width * _height) return;

                currentBlock = choosen;
            }
            else if (_blockDataStack.Count > 0)
            {
                currentBlock = _blockDataStack.Pop();
            }

            CalculatePath(currentBlock);
        }

        private void IncreaseEscapeRoute(BlockData currentBlock)
        {
            if (Random.Range(0, _randomRoutePossibility) == 0)
            {
                int index = Random.Range(0, _neighbours.Count);
                var choosen = _neighbours[index];
                UpdateWallSideActive(currentBlock, choosen);
            }
        }

        private void UpdateNeighbours(BlockData currentBlock)
        {
            if (currentBlock.Index.x - 1 >= 0)
            {
                var topNb = _blockDataArray[currentBlock.Index.x - 1, currentBlock.Index.z];
                if (!topNb.IsVisit) _neighbours.Add(topNb);
            }
            if (currentBlock.Index.x + 1 < _width)
            {
                var bottomNb = _blockDataArray[currentBlock.Index.x + 1, currentBlock.Index.z];
                if (!bottomNb.IsVisit) _neighbours.Add(bottomNb);
            }
            if (currentBlock.Index.z - 1 >= 0)
            {
                var leftNb = _blockDataArray[currentBlock.Index.x, currentBlock.Index.z - 1];
                if (!leftNb.IsVisit) _neighbours.Add(leftNb);
            }
            if (currentBlock.Index.z + 1 < _height)
            {
                var rightNb = _blockDataArray[currentBlock.Index.x, currentBlock.Index.z + 1];
                if (!rightNb.IsVisit) _neighbours.Add(rightNb);
            }
        }

        private void UpdateWallSideActive(BlockData current, BlockData choosen)
        {
            if (choosen.Index.x < current.Index.x)
            {
                current.SetActiveSide(WallSide.TOP, false);
                choosen.SetActiveSide(WallSide.BOTTOM, false);
            }
            else if (choosen.Index.x > current.Index.x)
            {
                current.SetActiveSide(WallSide.BOTTOM, false);
                choosen.SetActiveSide(WallSide.TOP, false);
            }
            else if (choosen.Index.z < current.Index.z)
            {
                current.SetActiveSide(WallSide.LEFT, false);
                choosen.SetActiveSide(WallSide.RIGHT, false);
            }
            else if (choosen.Index.z > current.Index.z)
            {
                current.SetActiveSide(WallSide.RIGHT, false);
                choosen.SetActiveSide(WallSide.LEFT, false);
            }
        }
    }
}