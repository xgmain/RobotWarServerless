using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using RobotWarServerless.Models;
using RobotWarServerless.Services;
using System.Net;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RobotWarServerless
{
    public class Function
    {
        private readonly RobotService _robotService;

        public Function()
        {
            _robotService = new RobotService();
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                context.Logger.LogLine("Processing request...");

                var robotInput = JsonConvert.DeserializeObject<RobotInput>(request.Body);
                var result = _robotService.ProcessRobotMovements(robotInput);

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(result),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error processing request: {ex.Message}");

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = "Error processing robot movements",
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
                };
            }
        }
    }
}
