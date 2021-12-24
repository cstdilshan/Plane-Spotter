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
    [Route("[controller]")]
    [Authorize]
    public class PlaneSpotterController : Controller
    {
        private ISpotterService _spotterService { get; set; }
        public PlaneSpotterController(ISpotterService spotterService)
        {
            this._spotterService = spotterService;
        }
        //[AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateSpotter([FromBody] SpotterViewModel viewModel)
        {
            var result = await _spotterService.Create(viewModel);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Error occurd in registration");
        }
        //[AllowAnonymous]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateSpotter([FromBody] SpotterViewModel viewModel)
        {
            var result = await _spotterService.Update(viewModel);
            if(result)
            {
                return Ok();
            }
            return BadRequest("Error occurd in update");
        }
        [Authorize]
        //[AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var spotters = await _spotterService.GetAll();
            if (spotters == null)
            {
                return NotFound();
            }
            return Ok(spotters);
        }
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var spotter = await _spotterService.GetById(id);
            if (spotter == null)
            {
                return NotFound();
            }
            return Ok(spotter);
        }

    }
}
