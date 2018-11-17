using AspNetCore.Homework.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Homework.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserIdentityDbContext _userIdentityDbContext;

        public UsersController(UserIdentityDbContext userIdentityDbContext)
        {
            _userIdentityDbContext = userIdentityDbContext;
        }
        // GET
        public IActionResult Index()
        {
            return  View(_userIdentityDbContext.Users);
        }
    }
}