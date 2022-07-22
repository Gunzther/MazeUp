using Map.Algorithm;
using Map.Data;
using Map.Object;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private Block _blockPrototype;

        public BlockData[,] BlockDataArray => _blockDataArray;
        private BlockData[,] _blockDataArray;
        private Dictionary<BlockData, Block> _currentBlocks;
        private float _blockSize;
        private int _mapSize;

        public BlockData GetBlockByPoint(float xPos, float zPos)
        {
            if (_blockDataArray == null) return null;

            int x = Mathf.Clamp(Mathf.FloorToInt(-xPos / _blockSize), 0, _mapSize - 1);
            int z = Mathf.Clamp(Mathf.FloorToInt(-zPos / _blockSize), 0, _mapSize - 1);

            return _blockDataArray[x, z];
        }

        public void DebugPath(List<BlockData> path)
        {
            ClearDebugPath();

            string str = "";

            foreach (var data in path)
            {
                str += $"{data.Index} > ";

                if (_currentBlocks.TryGetValue(data, out Block block))
                {
                    block.SetDebugFloorActive(true);
                }
            }

            Debug.Log(str);
        }

        public void ClearDebugPath()
        {
            foreach (var block in _currentBlocks.Values)
            {
                block.SetDebugFloorActive(false);
            }
        }

        public void ClearAll()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void GenerateMap(int mapSize)
        {
            _mapSize = mapSize;
            _blockSize = _blockPrototype.transform.localScale.x;
            _blockSize -= _blockSize / 10f; // remove double border

            _blockDataArray = CreateBlockDataArray(_mapSize, _mapSize, _blockSize);
            DepthFirstSearchAlgorithm algo = new DepthFirstSearchAlgorithm(_blockDataArray);
            _blockDataArray = algo.CalculatePath();

            float yPos = _blockPrototype.transform.position.y;
            _currentBlocks = new Dictionary<BlockData, Block>();

            foreach (var data in _blockDataArray)
            {
                Block block = Instantiate(_blockPrototype, new Vector3(data.Position.x, yPos, data.Position.z), Quaternion.identity, transform);
                block.Setup(data);
                block.SetActive(true);
                _currentBlocks.Add(data, block);
            }
        }

        private BlockData[,] CreateBlockDataArray(int width, int height, float blockSize)
        {
            BlockData[,] blockDataArray = new BlockData[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var blockData = new BlockData(_blockSize);
                    blockData.SetIndex(i, j);
                    blockData.SetPosition(x: -i * blockSize, z: -j * blockSize);
                    blockDataArray[i, j] = blockData;
                }
            }

            return blockDataArray;
        }
    }
}