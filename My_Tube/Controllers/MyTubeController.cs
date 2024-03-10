using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My_Tube.Core.IServices;

namespace My_Tube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyTubeController : ControllerBase
    {
        private readonly IMyTubeClientService _service;

        public MyTubeController(IMyTubeClientService service)
        {
            _service = service;
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] string Name, [FromQuery] int MaxResult = 30)
        {
            var result = await _service.SearchAsync(Name, MaxResult);

            return Ok(result);
        }

        [HttpGet("channel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchChannel([FromRoute] string id, [FromQuery] int MaxResult = 30)
        {
            var result = await _service.SearchChannelAsync(id, MaxResult);

            return Ok(result);
        }
    }
}
