using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using System.Reactive.Linq;
using UserInfoGraphQL.Types;

namespace UserInfoGraphQL
{
    public class UserInfoSubscription : ObjectGraphType<object>
    {
        private readonly UserInfoData userInfoData;

        public UserInfoSubscription(UserInfoData data)
        {
            userInfoData = data;
            Name = "UserInfoSubscription";

            AddField(new FieldType
            {
                Name = "UserUnderCommand",
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userid" }
                ),
                Type = typeof(UserType),
                StreamResolver = new SourceStreamResolver<UserInfo>(UserInfoSubscribe)
            });
        }

        private IObservable<UserInfo> UserInfoSubscribe(IResolveFieldContext context)
        {
            int userid = context.GetArgument<int>("userid");
            return Observable.Start(() => GetUserInfoType(userid));
        }

        public UserInfo GetUserInfoType(int userid)
        {
            UserInfo userInfo = userInfoData.GetUserInfoByID(userid).Result;
            return userInfo;
        }
    }
}