using AutoMapper;
using FootballersCatalog.Api.Abstract;
using FootballersCatalog.Api.Dtos;
using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Entities;

namespace FootballersCatalog.Api.Services
{
    public class FootballersService(
        IFootballersRepository footballersRepository,
        ITeamsRepository teamsRepository,
        IMapper mapper
        ) : IFootballersService
    {
        public async Task<List<FootballersResponse>> All()
        {
            var footballers = await footballersRepository.All();

            return mapper.Map<List<FootballersResponse>>(footballers);
        }

        public async Task<FootballersResponse> GetById(Guid id)
        {
            var footballers = await footballersRepository.GetById(id);
            return footballers == null 
                ? throw new Exception($"Footballer with id {id} not found") 
                : mapper.Map<FootballersResponse>(footballers);
        }

        public async Task<List<FootballersResponse>> GetByPage(int page, int pageSize)
        {
            var footballers = await footballersRepository.GetByPage(page, pageSize);

            return mapper.Map<List<FootballersResponse>>(footballers);
        }

        public async Task Add(AddFootballerRequest footballer)
        {
            var team = await teamsRepository.GetById(footballer.TeamId);
            var footballerEntity = mapper.Map<FootballerEntity>(footballer);
            footballerEntity.TeamTitle = team?.TeamTitle;

            await footballersRepository.Add(footballerEntity);
        }

        public async Task Update(Guid id, UpdateFootballerRequest updatedFootballer)
        {
            var findedFootballer = await footballersRepository.GetById(id);
            
            findedFootballer.Name = updatedFootballer.Name;
            findedFootballer.Surname = updatedFootballer.Surname;
            findedFootballer.Gender = updatedFootballer.Gender;
            findedFootballer.Birthday = updatedFootballer.Birthday;
            findedFootballer.Country = updatedFootballer.Country;
            
            if(updatedFootballer.TeamId != findedFootballer.TeamId)
            {
                var team = await teamsRepository.GetById(updatedFootballer.TeamId);
                findedFootballer.TeamTitle = team.TeamTitle;
                findedFootballer.TeamId = updatedFootballer.TeamId;
            }

            await footballersRepository.Update(findedFootballer);
        }

        public async Task Delete(Guid id)
        {
            var footballer = await footballersRepository.GetById(id) 
                ?? throw new Exception($"Footballer with id {id} not found");

            await footballersRepository.Delete(id);
        }
    }
}
