using FootballersCatalog.Api.Abstract;
using FootballersCatalog.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FootballersCatalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FootballersController(IFootballersService footballersService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<FootballersResponse>>> All()
        {
            var footballers = await footballersService.All();

            return Ok(footballers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var footballer = await footballersService.GetById(id);

            return Ok(footballer);
        }

        [HttpGet("{page}:{pageSize}")]
        public async Task<ActionResult<List<FootballersResponse>>> GetByPage([FromRoute]
            int page, [FromRoute] int pageSize)
        {
            var footballers = await footballersService.GetByPage(page, pageSize);

            return Ok(footballers);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddFootballerRequest request)
        {
            await footballersService.Add(request);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateFootballerRequest request
            )
        {
            await footballersService.Update(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await footballersService.Delete(id);

            return Ok();
        }
    }
}
