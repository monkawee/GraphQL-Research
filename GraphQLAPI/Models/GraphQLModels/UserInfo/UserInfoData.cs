using GraphQLAPI.Services;
using System.Reactive.Linq;
using UserInfoGraphQL.Types;

namespace UserInfoGraphQL
{
    public class UserInfoData
    {
        private readonly IUserInfoService userInfoService;
        public UserInfoData()
        {
            userInfoService = new MockingUserInfoService();
        }

        public Task<List<UserInfo>> GetUserInfoList()
        {
            return Task.FromResult(userInfoService.GetUserInfoList());
        }

        public Task<List<UserInfo>> GetUserInfoByName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Task.FromResult(userInfoService.GetUserInfoList());

            return Task.FromResult(userInfoService.GetUserInfoList().Where(user => user.UserName.Contains(username)).ToList());
        }

        public Task<UserInfo?> GetUserInfoByID(int userid)
        {
            return Task.FromResult(userInfoService.GetUserInfoList().FirstOrDefault(user => user.UserID == userid));
        }

        public Task<UserInfo> AddUser(UserInfo user)
        {
            return Task.FromResult(userInfoService.AddUser(user));
        }

        public string GetLineofCommandByID(UserInfoData data, int lineCommandID)
        {
            string? name = data.GetUserInfoByID(lineCommandID)?.Result?.UserName;
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            return name;
        }

        public Task<UserInfo> GetLineOfCommandName(int lineOfCommandID)
        {
            UserInfo lineOfCommandName = userInfoService.GetUserInfoList()
                                                       .FirstOrDefault(user => user.UserID == lineOfCommandID) ?? new();
            return Task.FromResult(lineOfCommandName);
        }

        public IObservable<List<UserInfo>> SubScribeAllUnderCommand(int userid)
        {
            List<UserInfo> userInfoList = userInfoService.GetUserInfoList().Where(user => user.LineCommand == userid).ToList();
            return Observable.Start(() =>
            {
                return userInfoList;
            });
        }

        public Task<List<UserInfo>> AddUserGetAll(UserInfo user)
        {
            return Task.FromResult(userInfoService.AddUserGetAll(user));
        }

        public IEnumerable<UserInfoClass>? GetUserUnderCommand(UserInfoClass userInfoClass)
        {
            if (userInfoClass == null) return null;

            List<UserInfoClass> users = [];
            int? commander = userInfoClass.UserID;

            if (commander != null)
            {
                List<UserInfo> userUnderCommand = userInfoService.GetUserInfoList().Where(cmd => cmd.LineCommand == commander).ToList();
                foreach (var underCommand in userUnderCommand)
                {
                    users.Add(underCommand);
                }
            }

            return users;
        }

        public List<MemberAccount> GetUserAccounts(int userID)
        {
            return [];
        }
    }
}

public class AnotherUserInfoData
{
    private readonly IUserInfoService userInfoService;
    public AnotherUserInfoData()
    {
        userInfoService = new MockingUserInfoService();
    }

    public Task<List<UserInfo>> GetUserInfoList()
    {
        return Task.FromResult(userInfoService.GetUserInfoList());
    }

    public Task<List<UserInfo>> GetUserInfoByName(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return Task.FromResult(userInfoService.GetUserInfoList());

        return Task.FromResult(userInfoService.GetUserInfoList().Where(user => user.UserName.Contains(username)).ToList());
    }

    public Task<UserInfo?> GetUserInfoByID(int userid)
    {
        return Task.FromResult(userInfoService.GetUserInfoList().FirstOrDefault(user => user.UserID == userid));
    }

    public Task<UserInfo> AddUser(UserInfo user)
    {
        return Task.FromResult(userInfoService.AddUser(user));
    }

    public Task<UserInfo> GetLineOfCommandName(int lineOfCommandID)
    {
        UserInfo lineOfCommandName = userInfoService.GetUserInfoList()
                                                   .FirstOrDefault(user => user.UserID == lineOfCommandID) ?? new();
        return Task.FromResult(lineOfCommandName);
    }
}
