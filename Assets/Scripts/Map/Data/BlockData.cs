namespace Map.Data
{
    public class BlockData
    {
        public bool[] WallActiveSides { get; private set; }
        public (int x, int z) Index { get; private set; }
        public (float x, float z) Position { get; private set; }
        public bool IsVisit { get; private set; }

        public BlockData()
        {
            WallActiveSides = new bool[4] { true, true, true, true };
            IsVisit = false;
        }

        public void SetActiveSide(int side, bool isActive) => WallActiveSides[side] = isActive;
        public void SetIndex(int x, int z) => Index = (x, z);
        public void SetPosition(float x, float z) => Position = (x, z);
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