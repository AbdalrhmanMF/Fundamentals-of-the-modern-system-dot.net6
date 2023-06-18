using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MVCCore.Models;

namespace POS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestLocalizationController : ControllerBase
    {

        private readonly IStringLocalizer<TestLocalizationController> _localization;

        public TestLocalizationController(IStringLocalizer<TestLocalizationController> localization)
        {
            _localization = localization;
        }



        [HttpGet("{name}")]
        public IActionResult GET(string name)
        {
            var welcomeMSG = string.Format(_localization["welcomeParam"], name);
             return Ok(welcomeMSG);
        }


        [HttpPost]
        public IActionResult GET(CreateTestVM model)
        {
            var welcomeMSG = string.Format(_localization[""], model.Name);
            return Ok(welcomeMSG);
        }

    }
}
