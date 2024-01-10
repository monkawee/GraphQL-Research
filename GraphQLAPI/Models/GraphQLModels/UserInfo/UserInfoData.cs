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

        public Task<List<UserInfo>> AddUserGetAll(UserInfo user)
        {
            return Task.FromResult(userInfoService.AddUserGetAll(user));
        }

        public Task<List<MemberAccount>>? GetUserAccounts(UserInfo userInfo)
        {
            if (userInfo == null) return null;
            UserInfo? user = userInfoService.GetUserInfoList().FirstOrDefault(user => user.UserID == userInfo.UserID);
            if (user == null) return null;
            List<MemberAccount> accounts = user.MemberAccounts ?? [];
            if (accounts.Count == 0) return null;

            return Task.FromResult(accounts);
        }

        public Task<List<BalanceYear>>? GetBalanceYears(MemberAccount memberAccount)
        {
            if (memberAccount == null) return null;

            return Task.FromResult(memberAccount.BalanceYears);
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
