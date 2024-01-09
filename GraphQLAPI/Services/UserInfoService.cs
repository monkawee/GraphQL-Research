using System.Text.RegularExpressions;
using UserInfoGraphQL.Types;

namespace GraphQLAPI.Services
{
    public interface IUserInfoService
    {
        UserInfo AddUser(UserInfo userInfo);
        List<UserInfo> AddUserGetAll(UserInfo userInfo);
        UserInfo GetUserInfo(int userid);
        List<UserInfo> GetUserInfoList();
    }

    public class UserInfoService : IUserInfoService
    {
        public UserInfo AddUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public List<UserInfo> AddUserGetAll(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserInfo(int userid)
        {
            throw new NotImplementedException();
        }

        public List<UserInfo> GetUserInfoList()
        {
            return new List<UserInfo>();
        }
    }
    #region Mocking Class
    public partial class MockingUserInfoService : IUserInfoService
    {
        public List<UserInfo> UserInfoList = [];
        public List<MemberAccount> MemberAccounts = [];
        public List<BalanceYear> BalanceYears = [];

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
                    LineCommand = i > 1 ? new Random().Next(1, 3) + 1 : 0,
                    MemberAccounts = GenerateMemberAccount(i),
                });
            }
        }

        public List<UserInfo> GetUserInfoList() => UserInfoList;

        public UserInfo GetUserInfo(int userid) => UserInfoList.First(user => user.UserID == userid);

        public UserInfo AddUser(UserInfo userInfo)
        {
            userInfo.UserID = UserInfoList.Count + 1;
            userInfo.UserName = userInfo.UserName;
            userInfo.Password = userInfo.Password;
            userInfo.Email = string.IsNullOrWhiteSpace(userInfo.Email) ? $"user{UserInfoList.Count + 1:0#}@company.org" : userInfo.Email;
            userInfo.MobileNo = string.IsNullOrWhiteSpace(userInfo.MobileNo) ? NumberRegex().Replace("08xxxxxxxx", n => new Random().Next(0, 9).ToString()) : userInfo.MobileNo;
            userInfo.CitizenID = string.IsNullOrWhiteSpace(userInfo.CitizenID) ? NumberRegex().Replace("xxxxxxxxxxxxx", n => new Random().Next(0, 9).ToString()) : userInfo.CitizenID;
            userInfo.Address = string.IsNullOrWhiteSpace(userInfo.Address) ? $"address {UserInfoList.Count + 1:00#}" : userInfo.Address;
            userInfo.LineCommand = userInfo.LineCommand;
            UserInfoList.Add(userInfo);

            return userInfo;
        }

        public List<UserInfo> AddUserGetAll(UserInfo userInfo)
        {
            AddUser(userInfo);
            return UserInfoList;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public List<MemberAccount> GenerateMemberAccount(int userID)
        {
            string acc1 = NumberRegex().Replace("xxx-xxxxxx-x", n => new Random().Next(0, 9).ToString());
            string acc2 = NumberRegex().Replace("xxx-xxxxxx-x", n => new Random().Next(0, 9).ToString());
            string acc3 = NumberRegex().Replace("xxx-xxxxxx-x", n => new Random().Next(0, 9).ToString());

            List<MemberAccount> memberAccounts =
            [
                new MemberAccount { AccountID = acc1, AccountName = $"User{userID:0#} Account#1", UUID = Guid.NewGuid(), BalanceYears = GenerateBalanceYears(acc1) },
                new MemberAccount { AccountID = acc2, AccountName = $"User{userID:0#} Account#2", UUID = Guid.NewGuid(), BalanceYears = GenerateBalanceYears(acc2) },
                new MemberAccount { AccountID = acc3, AccountName = $"User{userID:0#} Account#3", UUID = Guid.NewGuid(), BalanceYears = GenerateBalanceYears(acc3) },
            ];

            return memberAccounts;
        }

        public List<BalanceYear> GenerateBalanceYears(string accountNo)
        {
            List<BalanceYear> listBalanceYear =
            [
                new BalanceYear { BalanceYearID = $"{accountNo}_{DateTime.Now.AddYears(-2).Year}", Year = DateTime.Now.AddYears(-2).Year.ToString(), Balance = decimal.Parse(NumberRegex().Replace("xxxxxx.xx", n => new Random().Next(0, 9).ToString())) },
                new BalanceYear { BalanceYearID = $"{accountNo}_{DateTime.Now.AddYears(-1).Year}", Year = DateTime.Now.AddYears(-1).Year.ToString(), Balance = decimal.Parse(NumberRegex().Replace("xxxxxx.xx", n => new Random().Next(0, 9).ToString())) },
                new BalanceYear { BalanceYearID = $"{accountNo}_{DateTime.Now.Year}", Year = DateTime.Now.Year.ToString(), Balance = decimal.Parse(NumberRegex().Replace("xxxxxx.xx", n => new Random().Next(0, 9).ToString())) },
            ];

            return listBalanceYear;
        }

        [GeneratedRegex(@"([^\W0-9])")]
        public static partial Regex NumberRegex();
    }
    #endregion
}
