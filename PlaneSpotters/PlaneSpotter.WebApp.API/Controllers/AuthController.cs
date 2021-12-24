using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlaneSpotters.Core.Configuration;
using PlaneSpotters.Core.Entities;
using PlaneSpotters.Core.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlaneSpotter.WebApp.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration Configuration { get; set; }
        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptionsSnapshot<AppSettings> appSettings, IConfiguration configuration)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this.Configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] SignInViewModel signInModel)
        {
            var user = await _userManager.FindByNameAsync(signInModel.UserName).ConfigureAwait(true);
            
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, false, true);
                if (!result.Succeeded)
                    return BadRequest("Username or password is incorrect");

                var appSettingsSection = Configuration.GetSection("AppSettings");
                var appSettings = appSettingsSection.Get<AppSettings>();

                var key = Encoding.ASCII.GetBytes(appSettings.Secret);

                var token = GenerateJSONWebToken();

                return Ok(new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token
                });


            }
            return BadRequest("User is not available");
        }
        private string GenerateJSONWebToken()
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("spotter.api", "planespotter",
              null,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
