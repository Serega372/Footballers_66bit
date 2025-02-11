using FootballersCatalog.Api.Abstract;
using FootballersCatalog.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FootballersCatalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamsController(ITeamsService teamsService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TeamsResponse>>> All()
        {
            var teams = await teamsService.All();

            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamsResponse>> GetById([FromRoute] Guid id)
        {
            var team = await teamsService.GetById(id);

            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<TeamsResponse>> Add([FromBody] AddTeamRequest request)
        {   
            var team = await teamsService.Add(request);
           
            return Ok(team);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await teamsService.Delete(id);

            return Ok();
        }
    }
}
