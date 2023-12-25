using GraphQL.Transport;
using GraphQLWeb.Models;
using Microsoft.Extensions.Options;

namespace GraphQLWeb.Services
{
    public interface IUserInfoService
    {
        UserInfo? AddUserInfo(UserInfo userInfo);
        List<UserInfo>? GetUserInfo();
    }

    public class UserInfoService : IUserInfoService
    {
        private readonly AppSettings appSettings;
        private readonly LocalServiceProvider localServiceProvider;

        public UserInfoService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> options)
        {
            localServiceProvider = new LocalServiceProvider(httpClientFactory);
            appSettings = options.Value;
        }

        public List<UserInfo>? GetUserInfo()
        {
            GraphQLResponse? users = localServiceProvider.PostAsync<GraphQLResponse>($"{appSettings.APIURL}",
                param: new GraphQLRequest
                {
                    Query = "query UserInfo { userInfo { userID userName password email mobileNo ... on UserInformation { citizenID address } } }",
                    OperationName = "UserInfo",
                    Variables = null,
                }).Result;

            if (users == null || users.Data == null) { return null; }

            return users.Data.UserInfo;
        }

        public UserInfo? AddUserInfo(UserInfo userInfo)
        {
            userInfo.UserID = $"{GetUserInfo()?.Count + 1}";
            string queryParam = $"mutation CreateUser {{ createUser ( user: {{ userName: \"{userInfo.UserName}\" password: \"{userInfo.Password}\" email: \"{userInfo.Email}\" mobileNo: \"{userInfo.MobileNo}\" citizenID: \"{userInfo.CitizenID}\" address: \"{userInfo.Address}\" }} ) {{ userID userName password email mobileNo }} }}";

            GraphQLResponse? users = localServiceProvider.PostAsync<GraphQLResponse>($"{appSettings.APIURL}",
                param: new GraphQLRequest
                {
                    Query = queryParam,
                    OperationName = "CreateUser",
                    Variables = null,
                }).Result;

            if (users == null || users.Data == null) { return null; }

            return users.Data.UserInfo.FirstOrDefault();
        }
    }
}
