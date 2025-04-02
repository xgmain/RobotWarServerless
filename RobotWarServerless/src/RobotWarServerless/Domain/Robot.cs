namespace RobotWarServerless.Domain
{
    public class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Orientation Orientation { get; private set; }
        private readonly Arena arena;

        public Robot(int x, int y, Orientation orientation, Arena arena)
        {
            if (!arena.IsPositionValid(x, y))
                throw new ArgumentException("Initial position is outside arena bounds");

            X = x;
            Y = y;
            Orientation = orientation;
            this.arena = arena;
        }

        public void ExecuteCommands(string commands)
        {
            foreach (var command in commands)
            {
                ExecuteCommand(command);
            }
        }

        private void ExecuteCommand(char command)
        {
            switch (command)
            {
                case 'L': TurnLeft(); break;
                case 'R': TurnRight(); break;
                case 'M': MoveForward(); break;
                default: throw new ArgumentException($"Invalid command: {command}");
            }
        }

        private void TurnLeft()
        {
            Orientation = Orientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.W => Orientation.S,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N,
                _ => throw new InvalidOperationException("Invalid orientation")
            };
        }

        private void TurnRight()
        {
            Orientation = Orientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.E => Orientation.S,
                Orientation.S => Orientation.W,
                Orientation.W => Orientation.N,
                _ => throw new InvalidOperationException("Invalid orientation")
            };
        }

        private void MoveForward()
        {
            var (newX, newY) = Orientation switch
            {
                Orientation.N => (X, Y + 1),
                Orientation.E => (X + 1, Y),
                Orientation.S => (X, Y - 1),
                Orientation.W => (X - 1, Y),
                _ => throw new InvalidOperationException("Invalid orientation")
            };

            if (!arena.IsPositionValid(newX, newY))
                throw new InvalidOperationException("Movement would take robot outside arena bounds");

            X = newX;
            Y = newY;
        }

        public override string ToString() => $"{X} {Y} {Orientation}";
    }

    public enum Orientation { N, E, S, W }
}
