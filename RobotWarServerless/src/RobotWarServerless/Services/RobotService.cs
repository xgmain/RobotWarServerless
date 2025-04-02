using RobotWarServerless.Models;

namespace RobotWarServerless.Services
{
    public class RobotService
    {
        public RobotResponse ProcessRobotMovements(RobotInput input)
        {
            var response = new RobotResponse();

            if (string.IsNullOrWhiteSpace(input?.ArenaSize))
            {
                return response;
            }

            var arenaSize = input.ArenaSize.Split(' ');
            if (arenaSize.Length != 2 ||
                !int.TryParse(arenaSize[0], out var maxX) ||
                !int.TryParse(arenaSize[1], out var maxY))
            {
                return response;
            }

            var arena = new Arena(maxX, maxY);

            foreach (var robotInstruction in input.Robots)
            {
                if (string.IsNullOrWhiteSpace(robotInstruction.Position))
                    continue;

                var positionParts = robotInstruction.Position.Split(' ');
                if (positionParts.Length != 3 ||
                    !int.TryParse(positionParts[0], out var x) ||
                    !int.TryParse(positionParts[1], out var y) ||
                    positionParts[2].Length != 1)
                {
                    continue;
                }

                var orientation = positionParts[2][0];
                var commands = robotInstruction.Commands ?? string.Empty;

                var robot = new Robot(x, y, orientation, arena);
                robot.ExecuteCommands(commands);
                response.FinalPositions.Add(robot.ToString());
            }

            return response;
        }
    }
}
