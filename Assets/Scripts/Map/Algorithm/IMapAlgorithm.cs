using Map.Data;

namespace Map.Algorithm
{
    public interface IMapAlgorithm
    {
        public BlockData[,] CalculatePath();
    }
}