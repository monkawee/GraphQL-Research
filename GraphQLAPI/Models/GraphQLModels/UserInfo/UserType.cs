using GraphQL.Types;

namespace UserInfoGraphQL.Types
{
    public abstract class UserInfoClass
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
    }

    public class UserInfo : UserInfoClass
    {
        public string CitizenID { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int LineCommand { get; set; }
    }

    public class UserType : ObjectGraphType<UserInfo>
    {
        public UserType(UserInfoData data)
        {
            Name = "UserInformation";
            Description = "User Information";

            Field(u => u.UserID).Description("User ID");
            Field(u => u.UserName).Description("User name");
            Field(u => u.Password).Description("Password");
            Field(u => u.Email).Description("Email");
            Field(u => u.MobileNo).Description("Mobile No.");
            Field(u => u.CitizenID).Description("Citizen ID");
            Field(u => u.Address).Description("Address");
            Field("lineOfCommand", u => GetLineofCommandByID(data, u.LineCommand), nullable: true);

            Interface<UserInfoInterface>();
        }

        private static string GetLineofCommandByID(UserInfoData data, int lineCommandID)
        {
            string? name = data.GetUserInfoByID(lineCommandID)?.Result?.UserName;
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            return name;
        }
    }

    public class UserInfoInterface : InterfaceGraphType<UserInfoClass>
    {
        public UserInfoInterface()
        {
            Name = "UserInfo";
            Description = "User Info";

            Field(u => u.UserID).Description("User ID");
            Field(u => u.UserName).Description("User name");
            Field(u => u.Password).Description("Password");
            Field(u => u.Email).Description("Email");
            Field(u => u.MobileNo).Description("Mobile No.");
        }
    }
}