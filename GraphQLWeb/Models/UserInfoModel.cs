namespace GraphQLWeb.Models
{
    public class UserInfoClass
    {
        public string UserID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
    }

    public class UserInfo : UserInfoClass
    {
        public string CitizenID { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LineOfCommand { get; set; } = string.Empty;
    }

    public class UserInfoGraphQLModel
    {
        public List<UserInfo> UserInfo { get; set; } = new List<UserInfo>();
    }

    public class GraphQLResponse
    {
        public UserInfoGraphQLModel Data { get; set; } = new UserInfoGraphQLModel();
    }
}
