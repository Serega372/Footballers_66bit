using FootballersCatalog.Api.Dtos;

namespace FootballersCatalog.Api.Abstract
{
    public interface ITeamsService
    {
        Task<TeamsResponse> Add(AddTeamRequest request);
        Task Delete(Guid id);
        Task<List<TeamsResponse>> All();
        Task<TeamsResponse> GetById(Guid id);
    }
}