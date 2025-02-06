using FootballersCatalog.Api.Dtos;

namespace FootballersCatalog.Api.Abstract
{
    public interface IFootballersService
    {
        Task Add(AddFootballerRequest footballer);
        Task Update(Guid id, UpdateFootballerRequest updatedFootballer);
        Task Delete(Guid id);
        Task<List<FootballersResponse>> All();
        Task<FootballersResponse> GetById(Guid id);
        Task<List<FootballersResponse>> GetByPage(int page, int pageSize);
    }
}