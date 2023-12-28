using GraphQL;
using GraphQL.Types;
using UserInfoGraphQL.Types;

namespace UserInfoGraphQL
{
    public class UserInfoMutation : ObjectGraphType
    {
        public UserInfoMutation(UserInfoData data)
        {
            Name = "Mutation";
            Field<ListGraphType<UserType>>("CreateUser")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "User" }))
                .ResolveAsync(async context =>
                {
                    var user = context.GetArgument<UserInfo>("User");
                    return await data.AddUserGetAll(user);
                });
        }
    }
}
