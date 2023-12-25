using GraphQLAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        public UserInfoController()
        {
            _userInfoService = new MockingUserInfoService();
        }

        [HttpGet]
        public IActionResult GetUserInfoList()
        {
            return new JsonResult(_userInfoService.GetUserInfoList());
        }
    }
}
