using ApiUsersTest.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Linq;

namespace ApiUsersTest.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        public JsonResult GetUsers()
        {
            var users = _userManager.GetUsers();
            return new JsonResult(users.Select(x => new { x.FullName, x.Username }));
        }

        [HttpGet("GetByFilter")]
        public JsonResult GetUserByFilter(string filter, string orderByDescending)
        {
            if(filter == null)
            {
                return new JsonResult("Filters empty") { StatusCode = 404 };
            }

            try
            {
                var users = _userManager.GetUsersByFilter(filter, orderByDescending == "1");
                return new JsonResult(users.Select(x => new { x.FullName, x.Username }));
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message) { StatusCode = 404 };
            }
        }
    }
}
