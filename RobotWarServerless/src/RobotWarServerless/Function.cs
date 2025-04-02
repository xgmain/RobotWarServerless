using Amazon.Lambda.Core;
using RobotWarServerless.Application;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RobotWars.Lambda
{
    public class Function
    {
        private readonly IRobotCommandProcessor _commandProcessor;

        public Function()
        {
            // Initialize with default processor (could use DI in production)
            _commandProcessor = new RobotCommandProcessor();
        }

        // Constructor for testing with mock processor
        public Function(IRobotCommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public List<string> FunctionHandler(string input, ILambdaContext context)
        {
            try
            {
                context.Logger.LogInformation($"Processing input: {input}");
                var results = _commandProcessor.ProcessCommands(input).ToList();
                context.Logger.LogInformation($"Returning results: {string.Join(", ", results)}");
                return results;
            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Error processing commands: {ex.Message}");
                throw;
            }
        }
    }
}