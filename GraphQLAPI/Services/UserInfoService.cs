using System.Text.RegularExpressions;
using UserInfoGraphQL.Types;

namespace GraphQLAPI.Services
{
    public interface IUserInfoService
    {
        UserInfo AddUser(UserInfo userInfo);
        List<UserInfo> GetUserInfoList();
    }

    public class UserInfoService : IUserInfoService
    {
        public UserInfo AddUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public List<UserInfo> GetUserInfoList()
        {
            return new List<UserInfo>();
        }
    }
    #region Mocking Class
    public class MockingUserInfoService : IUserInfoService
    {
        public List<UserInfo> UserInfoList = new();
        public MockingUserInfoService()
        {
            List<UserInfo> userInfo = new();
            for (int i = 1; i <= 10; i++)
            {
                UserInfoList.Add(new UserInfo
                {
                    UserID = i,
                    UserName = $"User{i:0#}",
                    Password = RandomString(8),
                    Email = $"user{i:0#}@company.org",
                    MobileNo = Regex.Replace("08xxxxxxxx", @"([^0-9])", n => new Random().Next(0, 9).ToString()),
                    CitizenID = Regex.Replace("xxxxxxxxxxxxx", @"([^0-9])", n => new Random().Next(0, 9).ToString()),
                    Address = $"address {i:00#}",
                    LineCommand = i > 1 ? new Random().Next(1, 3) + 1 : 0
                });
            }
        }

        public List<UserInfo> GetUserInfoList() => UserInfoList;

        public UserInfo AddUser(UserInfo userInfo)
        {
            UserInfoList.Add(new UserInfo
            {
                UserID = UserInfoList.Count + 1,
                UserName = userInfo.UserName,
                Password = userInfo.Password,
                Email = string.IsNullOrWhiteSpace(userInfo.Email) ? $"user{UserInfoList.Count + 1:0#}@company.org" : userInfo.Email,
                MobileNo = string.IsNullOrWhiteSpace(userInfo.MobileNo) ? Regex.Replace("08xxxxxxxx", @"([^0-9])", n => new Random().Next(0, 9).ToString()) : userInfo.MobileNo,
                CitizenID = string.IsNullOrWhiteSpace(userInfo.CitizenID) ? Regex.Replace("xxxxxxxxxxxxx", @"([^0-9])", n => new Random().Next(0, 9).ToString()) : userInfo.CitizenID,
                Address = string.IsNullOrWhiteSpace(userInfo.Address) ? $"address {UserInfoList.Count + 1:00#}" : userInfo.Address,
                LineCommand = userInfo.LineCommand
            });
            return userInfo;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
    #endregion
}
