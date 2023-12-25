using GraphQLWeb.Models;
using GraphQLWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace GraphQLWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        public HomeController(IHttpClientFactory httpClientFactory, IOptions<AppSettings> options)
        {
            _userInfoService = new UserInfoService(httpClientFactory, options);
        }

        public IActionResult Index()
        {
            List<UserInfo>? users = _userInfoService.GetUserInfo();
            return View(users);
        }

        public IActionResult AddUserInfo()
        {
            return View();
        }

        public IActionResult Adduser(UserInfo userInfo)
        {
            UserInfo? newUser = _userInfoService.AddUserInfo(userInfo);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
