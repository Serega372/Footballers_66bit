using AutoMapper;
using FootballersCatalog.Api.Abstract;
using FootballersCatalog.Api.Dtos;
using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Entities;

namespace FootballersCatalog.Api.Services
{
    public class TeamsService(
        ITeamsRepository teamsRepository,
        IMapper mapper
        ) : ITeamsService
    {
        public async Task<List<TeamsResponse>> All()
        {
            var teams = await teamsRepository.All();

            return mapper.Map<List<TeamsResponse>>(teams);
        }

        public async Task<TeamsResponse> GetById(Guid id)
        {
            var team = await teamsRepository.GetById(id);
            return team == null 
                ? throw new Exception($"Team with id {id} not found") 
                : mapper.Map<TeamsResponse>(team);
        }

        public async Task<TeamsResponse> Add(AddTeamRequest request)
        {
            var teamEntity = mapper.Map<TeamEntity>(request);
            var team = await teamsRepository.Add(teamEntity);
            var response = mapper.Map<TeamsResponse>(team);
            return response;
        }

        public async Task Delete(Guid id)
        {
            var team = await teamsRepository.GetById(id) 
                ?? throw new Exception($"Team with id {id} not found");
            await teamsRepository.Delete(id);
        }
    }
}
