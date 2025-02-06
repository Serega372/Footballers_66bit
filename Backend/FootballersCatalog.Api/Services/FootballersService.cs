using AutoMapper;
using FootballersCatalog.Api.Abstract;
using FootballersCatalog.Api.Dtos;
using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Entities;
using Microsoft.AspNetCore.Components.Routing;

namespace FootballersCatalog.Api.Services
{
    public class FootballersService(
        IFootballersRepository footballersRepository,
        ITeamsRepository teamsRepository,
        IMapper mapper) : IFootballersService
    {
        private readonly IFootballersRepository _footballersRepository = footballersRepository;
        private readonly ITeamsRepository _teamsRepository = teamsRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<FootballersResponse>> All()
        {
            var footballers = await _footballersRepository.All();

            return _mapper.Map<List<FootballersResponse>>(footballers);
        }

        public async Task<FootballersResponse> GetById(Guid id)
        {
            var footballers = await _footballersRepository.GetById(id);
            return footballers == null 
                ? throw new Exception($"Footballer with id {id} not found") 
                : _mapper.Map<FootballersResponse>(footballers);
        }

        public async Task<List<FootballersResponse>> GetByPage(int page, int pageSize)
        {
            var footballers = await _footballersRepository.GetByPage(page, pageSize);

            return _mapper.Map<List<FootballersResponse>>(footballers);
        }

        public async Task Add(AddFootballerRequest footballer)
        {
            var team = await _teamsRepository.GetById(footballer.TeamId);
            var footballerEntity = _mapper.Map<FootballerEntity>(footballer);
            footballerEntity.TeamTitle = team?.TeamTitle;

            await _footballersRepository.Add(footballerEntity);
        }

        public async Task Update(Guid id, UpdateFootballerRequest updatedFootballer)
        {
            var findedFootballer = await _footballersRepository.GetById(id);
            
            findedFootballer.Name = updatedFootballer.Name;
            findedFootballer.Surname = updatedFootballer.Surname;
            findedFootballer.Gender = updatedFootballer.Gender;
            findedFootballer.Birthday = updatedFootballer.Birthday;
            findedFootballer.Country = updatedFootballer.Country;
            
            if(updatedFootballer.TeamId != findedFootballer.TeamId)
            {
                var team = await _teamsRepository.GetById(updatedFootballer.TeamId);
                findedFootballer.TeamTitle = team.TeamTitle;
            }

            await _footballersRepository.Update(findedFootballer);
        }

        public async Task Delete(Guid id)
        {
            var footballer = await _footballersRepository.GetById(id) 
                ?? throw new Exception($"Footballer with id {id} not found");

            await _footballersRepository.Delete(id);
        }
    }
}
