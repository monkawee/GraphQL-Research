using GraphQL.Instrumentation;
using GraphQL.Types;

namespace UserInfoGraphQL
{
    public class UserInfoSchema : Schema
    {
        public UserInfoSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetService(typeof(UserInfoQuery)) as UserInfoQuery ?? throw new InvalidOperationException();
            Mutation = provider.GetService(typeof(UserInfoMutation)) as UserInfoMutation ?? throw new InvalidOperationException();
            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }

    public class AnotherUserInfoSchema : Schema
    {
        public AnotherUserInfoSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetService(typeof(AnotherUserInfoQuery)) as AnotherUserInfoQuery ?? throw new InvalidOperationException();
            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }
}
