using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Transport;
using GraphQL.Types;
using GraphQLAPI.Helpers;
using GraphQLAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GraphQLAPI.Controllers
{
    [Route("graphql/")]
    public class GraphQLController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly GraphQLSettings _graphQLOptions;

        public GraphQLController(IDocumentExecuter documentExecuter, ISchema schema, IOptions<GraphQLSettings> options)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _graphQLOptions = options.Value;
        }

        [HttpPost("UserInfo")]
        public async Task<IActionResult> UserInfo([FromBody] GraphQLRequest request)
        {
            var result = await _documentExecuter.ExecuteAsync(s =>
            {
                s.Schema = _schema;
                s.Query = request.Query;
                s.Variables = request.Variables;
                s.OperationName = request.OperationName;
                s.RequestServices = HttpContext.RequestServices;
                s.UserContext = new GraphQLUserContext
                {
                    User = HttpContext.User,
                };
                s.CancellationToken = HttpContext.RequestAborted;
            });

            if (_graphQLOptions.EnableMetrics)
            {
                result.EnrichWithApolloTracing(DateTime.UtcNow);
            }

            return new ExecutionResultActionResult(result);
        }

        [HttpPost("AnotherUserInfo")]
        public async Task<IActionResult> AnotherUserInfo([FromBody] GraphQLRequest request)
        {
            var result = await _documentExecuter.ExecuteAsync(s =>
            {
                s.Schema = _schema;
                s.Query = request.Query;
                s.Variables = request.Variables;
                s.OperationName = request.OperationName;
                s.RequestServices = HttpContext.RequestServices;
                s.UserContext = new GraphQLUserContext
                {
                    User = HttpContext.User,
                };
                s.CancellationToken = HttpContext.RequestAborted;
            });

            if (_graphQLOptions.EnableMetrics)
            {
                result.EnrichWithApolloTracing(DateTime.UtcNow);
            }

            return new ExecutionResultActionResult(result);
        }
    }
}
