using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaneSpotters.Core.Interfaces;
using PlaneSpotters.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaneSpotter.WebApp.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private IAccountService _accountService { get; set; }
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
        {
            var result = await _accountService.RegisterUser(viewModel);
            if(result)
            {
                return Ok();
            }
            return BadRequest("Error occurd in registration");
            
        }
        //[AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _accountService.GetAll();
            return Ok(users);
        }
    }
}
