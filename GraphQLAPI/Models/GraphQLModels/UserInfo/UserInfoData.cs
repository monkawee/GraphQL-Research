using GraphQLAPI.Services;
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

        public Task<UserInfo> GetLineOfCommandName(int lineOfCommandID)
        {
            UserInfo lineOfCommandName = userInfoService.GetUserInfoList()
                                                       .FirstOrDefault(user => user.UserID == lineOfCommandID) ?? new();
            return Task.FromResult(lineOfCommandName);
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
}