using AuthenticationWithIdentity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWithIdentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new AppUser()
            {
                FullName = model.FullName,
                Email = model.Email,
                Contact = model.Contact,
                Address = model.Address,
                UserName = model.Email,
                PasswordHash = model.Password

            };
            var result = await _userManager.CreateAsync(user, user.PasswordHash!);
            if (result.Succeeded)
            {
                return Ok("Registrated");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(string email, string password)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                userName: email!,
                password: password!,
                isPersistent:false, //Cookie browser o'chgandan kegin o'chirilsinmi
                lockoutOnFailure:false


               );
            if (signInResult.Succeeded)
            {
                return Ok(signInResult);
            }
            return BadRequest("Something went wrong");
        }

    }
}
