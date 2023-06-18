using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.BL.Interfaces;
using POS.BL.UOW;
using POS.DAL.Models;

//using POS.DAL.Models;


namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        //private readonly IBaseuofsitory<Source> _uof;
        private readonly IUnitOfWork _uof;
        public SourceController(IUnitOfWork uof /*IBaseuofsitory<Source> uof*/)
        {
            _uof = uof;
        }


        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            return Ok(_uof.Sources.GetById(id));
        }


        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _uof.Sources.GetByIdAsync(id));
        }


        [HttpGet("GetByNameAsync")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var data = await _uof.Sources.FindAsync(x => x.AName.Contains(name));
            if (data != null)
                return Ok(data);            
            else
                return BadRequest();            
        }

        [HttpGet("GetByNameIncludesAsync")]
        public async Task<IActionResult> GetByNameIncludesAsync(string name)
        {
            var data = await _uof.Sources.FindAsync(x => x.AName.Contains(name), new[] {"SourceType"});
            if (data != null)
                return Ok(data);
            else
                return BadRequest();
        }

        [HttpGet("GetAllByNameIncludesAsync")]
        public async Task<IActionResult> GetAllByNameIncludesAsync(string name)
        {
            var data = await _uof.Sources.FindAllAsync(x => x.AName.Contains(name), new[] {"SourceType"});
            if (data != null)
                return Ok(data);
            else
                return BadRequest();
        }

        [HttpPost("AddingSource")]
        public async Task<IActionResult> AddingSource(Source model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(await _uof.Sources.AddAsync(model));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(model);
            }
        }
    }
}
