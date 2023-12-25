using GraphQL;
using GraphQL.Types;
using UserInfoGraphQL.Types;

namespace UserInfoGraphQL
{
    public class UserInfoQuery : ObjectGraphType<object>
    {
        public UserInfoQuery(UserInfoData data)
        {
            Name = "Query";
            Field<ListGraphType<UserInfoInterface>>("UserInfo").ResolveAsync(async context => await data.GetUserInfoList());
            Field<ListGraphType<UserType>>("GetUserByName")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username", Description = "Search user by name" }))
                .ResolveAsync(async context => await data.GetUserInfoByName(context.GetArgument<string>("username")));
            Field<UserType>("GetUserByID")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userid", Description = "Search user by  ID" }))
                .ResolveAsync(async context => await data.GetUserInfoByID(context.GetArgument<int>("userid")));
        }
    }

    public class AnotherUserInfoQuery : ObjectGraphType<object>
    {
        public AnotherUserInfoQuery(UserInfoData data)
        {
            Name = "AnotherQuery";
            Field<ListGraphType<UserInfoInterface>>("AnotherUserInfo").ResolveAsync(async context => await data.GetUserInfoList());
            Field<ListGraphType<UserType>>("AnotherGetUserByName")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "username", Description = "Search user by name" }))
                .ResolveAsync(async context => await data.GetUserInfoByName(context.GetArgument<string>("username")));
            Field<UserType>("AnotherGetUserByID")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userid", Description = "Search user by  ID" }))
                .ResolveAsync(async context => await data.GetUserInfoByID(context.GetArgument<int>("userid")));
        }
    }
}
