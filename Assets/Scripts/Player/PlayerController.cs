using Map;
using Map.Data;
using PathFinding.Algorithm;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private MapGenerator _mapGenerator;

        [SerializeField]
        private Transform _tempBot;

        [SerializeField]
        private int _speed = 2;

        private AStarAlgorithm _pathFinder;

        private void Start()
        {
            _pathFinder = new AStarAlgorithm(_mapGenerator.BlockDataArray);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += -transform.right * _speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += -transform.forward * _speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * _speed * Time.deltaTime;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                BlockData playerData = _mapGenerator.GetBlockByPoint(transform.position.x, transform.position.z);
                BlockData botData = _mapGenerator.GetBlockByPoint(_tempBot.position.x, _tempBot.position.z);
                Debug.Log($"player: ({playerData.Index.x}, {playerData.Index.z}) bot: ({botData.Index.x}, {botData.Index.z})");

                var path = _pathFinder.FindShortestPath(botData, playerData);
                var str = "";

                foreach(var p in path)
                {
                    str += $"{p.Index} > ";
                }

                _mapGenerator.DebugPath(path);
            }
        }
    }
}