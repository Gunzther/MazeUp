using System;

namespace Timer
{
    /// <summary>
    /// Timer to countdown time in seconds
    /// </summary>
    public class CountdownTimer
    {
        public event Action<int> OnTimeUpdated;
        public event Action OnTimeEnd;

        private int _seconds = 0;
        private float _timer = 0;
        private bool _isCount = false;

        /// <param name="seconds">countdown time in seconds</param>
        public void Start(int seconds)
        {
            _seconds = seconds;
            _timer = 0;
            _isCount = true;
            OnTimeUpdated?.Invoke(_seconds);
        }

        public void Stop()
        {
            _seconds = 0;
            _timer = 0;
            _isCount = false;
        }

        public void Continue() => _isCount = true;

        public void Pause() => _isCount = false;

        /// <summary>
        /// Call this in mono update
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            if (_isCount)
            {
                _timer += deltaTime;

                if (_timer >= 1)
                {
                    _seconds--;
                    OnTimeUpdated?.Invoke(_seconds);
                    _timer = 0;
                }
                if (_seconds <= 0)
                {
                    Stop();
                    OnTimeEnd?.Invoke();
                }
            }
        }
    }
}