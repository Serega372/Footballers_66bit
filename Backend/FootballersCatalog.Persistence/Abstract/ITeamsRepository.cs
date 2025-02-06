using FootballersCatalog.Persistence.Entities;

namespace FootballersCatalog.Persistence.Abstract
{
    public interface ITeamsRepository
    {
        Task<TeamEntity> Add(TeamEntity team);
        Task Delete(Guid id);
        Task<List<TeamEntity>> All();
        Task<TeamEntity?> GetById(Guid id);
    }
}