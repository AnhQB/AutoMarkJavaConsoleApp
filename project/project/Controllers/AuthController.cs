using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project.DTO;
using project.Models;
using project.Services;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginDTO model)
        {
            Admin u = AdminService.GetSingleton().GetAdmin(model);

            if (u == null)
            {
                return Unauthorized();
            }

            // HttpContext.Session.SetString("Email", model.email);
            //HttpContext.Session.SetString("Role", user.RoleId.ToString());

            return Ok(u.Username);
        }
    }
}
