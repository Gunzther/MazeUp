using UnityEngine;

namespace Map.Data
{
    public class BlockData
    {
        public bool[] WallActiveSides { get; private set; }
        public (int x, int z) Index { get; private set; }
        public (float x, float z) Position { get; private set; }
        public Vector3 CenterPosition { get; private set; }
        public bool IsVisit { get; private set; }

        private float _offset;

        public BlockData(float blockSize)
        {
            WallActiveSides = new bool[4] { true, true, true, true };
            IsVisit = false;
            _offset = blockSize / 2;
        }

        public void SetActiveSide(int side, bool isActive) => WallActiveSides[side] = isActive;
        public void SetIndex(int x, int z) => Index = (x, z);
        public void SetPosition(float x, float z)
        {
            Position = (x, z);
            CenterPosition = new Vector3(x - _offset, 0, z - _offset);
        }

        public void SetIsVisit(bool isVisit) => IsVisit = isVisit;
    }

    public static class WallSide
    {
        public const int TOP = 0;
        public const int BOTTOM = 1;
        public const int LEFT = 2;
        public const int RIGHT = 3;
    }
}