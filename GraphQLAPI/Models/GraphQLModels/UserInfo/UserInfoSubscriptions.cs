using GraphQL.Resolvers;
using GraphQL.Types;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using UserInfoGraphQL.Types;

namespace UserInfoGraphQL
{
    public class UserInfoSubscriptions : ObjectGraphType<object>
    {
        private readonly UserInfoData userInfoData;
        private readonly ISubject<UserInfo> _userInfoStream = new ReplaySubject<UserInfo>(1);
        private readonly ISubject<List<UserInfo>> _allUserInfoStream = new ReplaySubject<List<UserInfo>>(1);

        ConcurrentStack<UserInfo> AllUserInfo { get; }
        public UserInfoSubscriptions(UserInfoData data)
        {
            userInfoData = data;
            AllUserInfo = new ConcurrentStack<UserInfo>();

            AddField(new FieldType
            {
                Name = "Subscriptions",
                Type = typeof(ListGraphType<UserType>),
                StreamResolver = new SourceStreamResolver<List<UserInfo>>(_ => GetAllUserInfo())
            });
        }

        public List<UserInfo> AddUserGetAll(UserInfo userInfo)
        {
            AllUserInfo.Push(userInfo);
            var l = new List<UserInfo>(AllUserInfo);
            _allUserInfoStream.OnNext(l);
            return l;
        }

        public UserInfo AddUser(UserInfo userInfo)
        {
            AllUserInfo.Push(userInfo);
            _userInfoStream.OnNext(userInfo);
            return userInfo;
        }

        public IObservable<List<UserInfo>> GetAllUserInfo()
        {
            return _allUserInfoStream.AsObservable();
        }

        public IObservable<UserInfo> GetUserInfo()
        {
            return _userInfoStream.AsObservable();
        }
    }
}
