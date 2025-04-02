namespace RobotWarServerless.Models
{
    public class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Orientation { get; private set; }
        private readonly Arena _arena;

        public Robot(int x, int y, char orientation, Arena arena)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            _arena = arena ?? throw new ArgumentNullException(nameof(arena));
        }

        public void ExecuteCommands(string commands)
        {
            if (commands == null) return;

            foreach (var command in commands)
            {
                switch (command)
                {
                    case 'L':
                        TurnLeft();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    case 'M':
                        MoveForward();
                        break;
                        // Ignore invalid commands
                }
            }
        }

        private void TurnLeft()
        {
            Orientation = Orientation switch
            {
                'N' => 'W',
                'W' => 'S',
                'S' => 'E',
                'E' => 'N',
                _ => Orientation
            };
        }

        private void TurnRight()
        {
            Orientation = Orientation switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => Orientation
            };
        }

        private void MoveForward()
        {
            var (newX, newY) = Orientation switch
            {
                'N' => (X, Y + 1),
                'E' => (X + 1, Y),
                'S' => (X, Y - 1),
                'W' => (X - 1, Y),
                _ => (X, Y)
            };

            if (_arena.IsWithinBounds(newX, newY))
            {
                X = newX;
                Y = newY;
            }
        }

        public override string ToString() => $"{X} {Y} {Orientation}";
    }
}
