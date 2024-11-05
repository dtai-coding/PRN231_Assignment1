using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;
using Services;
using Microsoft.AspNetCore.Authorization;

namespace PRN231PE_FA23_665511_taipdse172357.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SilverJewelriesController : ControllerBase
    {
        private readonly IJewelryService _jewelryService;

        public SilverJewelriesController(IJewelryService IJewelryService)
        {
            _jewelryService = IJewelryService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SilverJewelry>>> Get()
        {
            try
            {
                var jewelry = await _jewelryService.GetJewelries();
                return Ok(jewelry);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [HttpGet("/api/Category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCatergory()
        {
            return Ok(await _jewelryService.GetCategories());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SilverJewelry>> Details(string id)
        {
            try
            {
                var jewelry = await _jewelryService.GetJewelry(id);
                return Ok(jewelry);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [HttpPost]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<SilverJewelry>> Create([FromBody] SilverJewelry silverJewelry)
        {
            try
            {
                var newJewelry = await _jewelryService.AddSilverAsync(silverJewelry);

                return Ok(newJewelry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<SilverJewelry>> Edit(string id, [FromBody] SilverJewelry silverJewelry)
        {

            try
            {
                var jewelry = await _jewelryService.UpdateSilverAsync(silverJewelry);
                return Ok(jewelry);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<SilverJewelry>> Delete(string id)
        {
            try
            {
                var jewelry = await _jewelryService.DeleteJewelry(id);
                return Ok(jewelry);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
