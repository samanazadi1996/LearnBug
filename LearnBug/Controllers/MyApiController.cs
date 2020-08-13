using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ViewModels;

namespace LearnBug.Controllers
{
    public class MyApiController : ApiController
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public MyApiController(IJwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }
        [HttpGet]
        public ApiResult<string> GetToken(/*LoginUserViewModel model*/)
        {
            var user = new LoginUserViewModel()
            {
                Username = "admin",
                Password = "123"
            };

            if (!_userService.UserExist(user))
                return BadRequest();

            var result = _jwtService.GenerateAsync(user);
            return result.ToString();
        }
    }
}
