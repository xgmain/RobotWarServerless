namespace RobotWarServerless.Models
{
    public class Arena
    {
        public int MaxX { get; }
        public int MaxY { get; }

        public Arena(int maxX, int maxY)
        {
            if (maxX < 0 || maxY < 0)
                throw new ArgumentException("Arena dimensions must be positive");

            MaxX = maxX;
            MaxY = maxY;
        }

        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x <= MaxX && y >= 0 && y <= MaxY;
        }
    }
}
