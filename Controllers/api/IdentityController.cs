using System.Linq;
using Microsoft.AspNetCore.Mvc;
using aspnetcore_vue.Providers;
using aspnetcore_vue.Services;
using aspnetcore_vue.Models.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace aspnetcore_vue.Controllers.api
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        public IdentityService Service { get; }


        public IdentityController(IdentityService service)
        {
            Service = service;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]UserSignInModel model)
        {
            //System.Threading.Thread.Sleep(500); // Fake latency

            var result = await Service.SignIn(model.username, model.password);
            if (result.Succeeded)
            {
                return Ok(await Service.GetUserProfile(model.username));
            }
            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> LogOut()
        {
            await Service.SignOut();
            return Ok();
        }

        

        [HttpGet("[action]")]
        public IActionResult Test()
        {
            return Ok("hello");
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> WhoAmI()
        {
            var user = await Service.GetUserProfile(User.Identity.Name);
            return Ok(user);
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> RegisterTest()
        {
            var user = new UserRegisterModel
            {
                Username = "TestUser",
                Password = "Password!23",
                FirstName = "John",
                LastName = "Doe",
                Email = "testuser@test.com"
            };

            if ( await Service.Register(user) )
            {
                user.Password = string.Empty;
                return Ok(new { result = "Ok", user });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TestLogin()
        {
            var result = await Service.SignIn("testuser", "Password!23");
            if (result.Succeeded)
            {
                return Ok(await Service.GetUserProfile("testuser"));
            }
            return BadRequest();
        }
    }
}
