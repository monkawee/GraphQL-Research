using System.Security.Claims;

namespace GraphQLAPI.Models
{
    public class GraphQLSettings
    {
        public bool EnableMetrics { get; set; }
    }

    public class GraphQLUserContext : Dictionary<string, object?>
    {
        public ClaimsPrincipal User { get; set; } = new ClaimsPrincipal();
    }
}
