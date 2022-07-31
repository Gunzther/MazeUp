using Bot;
using Map;
using Map.Data;
using PathFinding;
using Player;
using System;
using System.Collections.Generic;
using Timer;
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
        private int _countdownSeconds = 75;

        [SerializeField]
        private bool _debugPath;

        /// <summary>
        /// true is win, false is lose
        /// </summary>
        public event Action<bool> OnGameEnd;

        public CountdownTimer Timer => _timer;
        private CountdownTimer _timer;
        
        public void StartGame()
        {
            _timer.Start(_countdownSeconds);

            var xPos = UnityEngine.Random.Range(0, _mapSize);
            var zPos = UnityEngine.Random.Range(0, _mapSize);
            _botController.SetPosition(_mapGenerator.BlockDataArray[xPos, zPos].CenterPosition);
            _botController.SetIsPlaying(true);
        }

        public void ResetGame()
        {
            _timer.Stop();
            _mapGenerator.ClearAll();
            _mapGenerator.GenerateMap(_mapSize);

            PathFinder pathFinder = new PathFinder(_mapGenerator, PathFinder.Algorithm.AStar);
            pathFinder.SetTranforms(from: _botController.transform, to: _playerController.transform);
            _botController.SetPathFinder(pathFinder);
        }

        private void Awake()
        {
            _timer = new CountdownTimer();
            _timer.OnTimeEnd += () => OnGameEndHandler(true);
            _botController.OnPlayerCaught += () => OnGameEndHandler(false);

            if (_debugPath) _botController.OnNewPathFound += OnFindNewPath;

            ResetGame();
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            if (_debugPath) _botController.OnNewPathFound -= OnFindNewPath;
        }

        private void OnFindNewPath(List<BlockData> path)
        {
            _mapGenerator.DebugPath(path);
        }

        private void OnGameEndHandler(bool isWin)
        {
            _botController.SetIsPlaying(false);
            OnGameEnd?.Invoke(isWin);
        }
    }
}