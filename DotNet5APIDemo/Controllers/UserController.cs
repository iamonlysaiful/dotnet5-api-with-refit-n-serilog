using DotNet5APIDemo.Models;
using DotNet5APIDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5APIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSvc;

        public UserController( IUserService userSvc)
        {
            _userSvc = userSvc;
        }

        [HttpGet("api/users")]
        public async Task<IEnumerable<User>> GetUsers(int page)
        {
            Log.Information("User Controller:GetUsers");
            return await _userSvc.GetUsers(page);
        }

        [HttpGet("api/user")]
        public async Task<User> GetUser(int id)
        {
            Log.Information("User Controller:GetUser");
            return await _userSvc.GetUserById(id);
        }
        
    }
}
