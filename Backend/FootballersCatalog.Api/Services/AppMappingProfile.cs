using AutoMapper;
using FootballersCatalog.Api.Dtos;
using FootballersCatalog.Persistence.Entities;

namespace FootballersCatalog.Api.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile() 
        {
            CreateMap<AddFootballerRequest, FootballerEntity>();
            CreateMap<UpdateFootballerRequest, FootballerEntity>();
            CreateMap<FootballerEntity, FootballersResponse>();

            CreateMap<TeamEntity, TeamsResponse>();
            CreateMap<AddTeamRequest, TeamEntity>();
        }
    }
}
