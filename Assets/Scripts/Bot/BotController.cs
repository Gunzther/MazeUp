using Map.Data;
using PathFinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bot
{
    public class BotController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        /// <summary>
        /// After bot walk through path[index] position,
        /// it will find the new shortest path.
        /// </summary>
        [SerializeField]
        private int _IndexToFindNext = 4;

        public event Action<List<BlockData>> OnNewPathFound;

        private const float _vectorOffset = 0.02f;

        private List<BlockData> _path;
        private PathFinder _pathFinder;
        private int _index = 0;
        private bool _isPlaying = false;

        public void SetPathFinder(PathFinder pathFinder) => _pathFinder = pathFinder;

        public void SetIsPlaying(bool isPlaying)
        {
            _isPlaying = isPlaying;
            if (_isPlaying) FindNewPath();
        }

        private void Update()
        {
            if (!_isPlaying) return;

            if (_index >= _IndexToFindNext)
            {
                FindNewPath();
            }
            if (IsEquals(transform.position, _path[_index].CenterPosition))
            {
                _index++;

                if (_index  >= _path.Count)
                {
                    FindNewPath();
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, _path[_index].CenterPosition, Time.deltaTime * _speed);
        }

        private void FindNewPath()
        {
            _path = _pathFinder.FindShortestPath();
            _index = 0;
            OnNewPathFound?.Invoke(_path);
        }

        private bool IsEquals(Vector3 pos1, Vector3 pos2)
        {
            return Vector3.Distance(pos1, pos2) <= _vectorOffset;
        }
    }
}