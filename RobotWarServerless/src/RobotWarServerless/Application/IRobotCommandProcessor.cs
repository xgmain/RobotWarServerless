namespace RobotWarServerless.Application
{
    public interface IRobotCommandProcessor
    {
        IEnumerable<string> ProcessCommands(string input);
    }
}