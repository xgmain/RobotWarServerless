namespace RobotWarServerless.Application
{
    public interface IRobotCommandProcessor
    {
        IEnumerable<string> Process(string input);
    }
}