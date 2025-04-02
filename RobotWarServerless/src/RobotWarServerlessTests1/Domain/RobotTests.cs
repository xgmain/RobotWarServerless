using RobotWarServerless.Domain;
using Xunit;

namespace RobotWarServerless.Domain.Tests
{
    public class RobotTests
    {
        private readonly Arena _arena = new Arena(5, 5);

        [Theory]
        [InlineData("0 0 N", "M", "0 1 N")]
        [InlineData("1 1 E", "M", "2 1 E")]
        [InlineData("1 1 S", "M", "1 0 S")]
        [InlineData("1 1 W", "M", "0 1 W")]
        public void MoveForward_ChangesPositionCorrectly(string startPos, string commands, string expectedPos)
        {
            var robot = CreateRobot(startPos);
            robot.ExecuteCommands(commands);
            Xunit.Assert.Equal(expectedPos, robot.ToString());
        }

        [Theory]
        [InlineData("0 0 N", "L", "0 0 W")]
        [InlineData("0 0 W", "L", "0 0 S")]
        [InlineData("0 0 S", "L", "0 0 E")]
        [InlineData("0 0 E", "L", "0 0 N")]
        public void TurnLeft_ChangesOrientationCorrectly(string startPos, string commands, string expectedPos)
        {
            var robot = CreateRobot(startPos);
            robot.ExecuteCommands(commands);
            Xunit.Assert.Equal(expectedPos, robot.ToString());
        }

        private Robot CreateRobot(string position)
        {
            var parts = position.Split(' ');
            return new Robot(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                Enum.Parse<Orientation>(parts[2]),
                _arena);
        }
    }
}