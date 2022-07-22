using Bot;
using Map;
using Map.Data;
using PathFinding;
using Player;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private BotController _botController;

        [SerializeField]
        private PlayerController _playerController;

        [SerializeField]
        private MapGenerator _mapGenerator;

        [SerializeField]
        private int _mapSize = 14;

        [SerializeField]
        private bool _debugPath;

        private void Awake()
        {
            _mapGenerator.GenerateMap(_mapSize);

            PathFinder pathFinder = new PathFinder(_mapGenerator, PathFinder.Algorithm.AStar);
            pathFinder.SetTranforms(from: _botController.transform, to: _playerController.transform);

            if (_debugPath) _botController.OnNewPathFound += OnFindNewPath;

            var xPos = Random.Range(0, _mapSize);
            var zPos = Random.Range(0, _mapSize);
            _botController.SetPosition(_mapGenerator.BlockDataArray[xPos, zPos].CenterPosition);
            _botController.SetPathFinder(pathFinder);
            _botController.SetIsPlaying(true);
        }

        private void OnDestroy()
        {
            if (_debugPath) _botController.OnNewPathFound -= OnFindNewPath;
        }

        private void OnFindNewPath(List<BlockData> path)
        {
            _mapGenerator.DebugPath(path);
        }
    }
}