using MercuryTest.Interfaces;
using MercuryTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace MercuryTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ACPDController : Controller
    {
        private readonly IACPDService _aCPDService;

        public ACPDController(IACPDService aCPDService)
        {
            _aCPDService = aCPDService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _aCPDService.READ();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{sid}")]
        public async Task<IActionResult> Get(string sid)
        {
            try
            {
                var result = await _aCPDService.READ(sid);
                return StatusCode(201, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MyOffice_ACPD acpd)
        {
            try
            {
                var result = await _aCPDService.Create(acpd);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{sid}")]
        public async Task<IActionResult> Put(string sid, [FromBody] MyOffice_ACPD acpd)
        {
            try
            {
                var result = await _aCPDService.Update(sid, acpd);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{sid}")]
        public async Task<IActionResult> Delete(string sid)
        {
            try
            {
                var result = await _aCPDService.Delete(sid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
