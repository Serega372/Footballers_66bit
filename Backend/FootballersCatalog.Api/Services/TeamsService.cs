using AutoMapper;
using FootballersCatalog.Api.Abstract;
using FootballersCatalog.Api.Dtos;
using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Entities;
using FootballersCatalog.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FootballersCatalog.Api.Services
{
    public class TeamsService(
        ITeamsRepository teamsRepository,
        IMapper mapper) : ITeamsService
    {
        private readonly ITeamsRepository _teamsRepository = teamsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<TeamsResponse>> All()
        {
            var teams = await _teamsRepository.All();

            return _mapper.Map<List<TeamsResponse>>(teams);
        }

        public async Task<TeamsResponse> GetById(Guid id)
        {
            var team = await _teamsRepository.GetById(id);
            return team == null 
                ? throw new Exception($"Team with id {id} not found") 
                : _mapper.Map<TeamsResponse>(team);
        }

        public async Task<TeamsResponse> Add(AddTeamRequest request)
        {
            var teamEntity = _mapper.Map<TeamEntity>(request);
            var team = await _teamsRepository.Add(teamEntity);
            var response = _mapper.Map<TeamsResponse>(team);
            return response;
        }

        public async Task Delete(Guid id)
        {
            var team = await _teamsRepository.GetById(id) 
                ?? throw new Exception($"Team with id {id} not found");
            await _teamsRepository.Delete(id);
        }
    }
}
