using Map.Algorithm;
using Map.Data;
using Map.Object;
using UnityEngine;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private Block _blockPrototype;

        [SerializeField]
        private int _mapSize;

        private BlockData[,] _blockDataArray;
        private float _blockSize;

        public BlockData GetBlockByPoint(float xPos, float zPos)
        {
            int x = Mathf.Clamp(Mathf.FloorToInt(-xPos /_blockSize), 0, _mapSize - 1);
            int z = Mathf.Clamp(Mathf.FloorToInt(-zPos /_blockSize), 0, _mapSize - 1);

            return _blockDataArray[x, z];
        }

        private void OnEnable()
        {
            GenerateMap();
        }

        private void OnDisable()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void GenerateMap()
        {
            _blockSize = _blockPrototype.transform.localScale.x;
            _blockSize -= _blockSize / 10f; // remove double border

            _blockDataArray = CreateBlockDataArray(_mapSize, _mapSize, _blockSize);
            DepthFirstSearchAlgorithm algo = new DepthFirstSearchAlgorithm(_blockDataArray);
            _blockDataArray = algo.CalculatePath();

            float yPos = _blockPrototype.transform.position.y;

            foreach (var data in _blockDataArray)
            {
                Block block = Instantiate(_blockPrototype, new Vector3(data.Position.x, yPos, data.Position.z), Quaternion.identity, transform);
                block.Setup(data);
                block.SetActive(true);
            }
        }

        private BlockData[,] CreateBlockDataArray(int width, int height, float blockSize)
        {
            BlockData[,] blockDataArray = new BlockData[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var blockData = new BlockData();
                    blockData.SetIndex(i, j);
                    blockData.SetPosition(x: -i * blockSize, z: -j * blockSize);
                    blockDataArray[i, j] = blockData;
                }
            }

            return blockDataArray;
        }
    }
}