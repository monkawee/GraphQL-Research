using GraphQL.Types;
using UserInfoGraphQL.Types;

namespace UserInfoGraphQL
{
    public class UserInputType : InputObjectGraphType<UserInfo>
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field(u => u.UserName);
            Field(u => u.Password);
            Field(u => u.Email, nullable: true);
            Field(u => u.MobileNo, nullable: true);
            Field(u => u.CitizenID, nullable: true);
            Field(u => u.Address, nullable: true);
            Field(u => u.LineCommand, nullable: true);
        }
    }
}
