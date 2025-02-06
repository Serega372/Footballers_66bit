using FootballersCatalog.Persistence.Entities;

namespace FootballersCatalog.Persistence.Abstract
{
    public interface IFootballersRepository
    {
        Task Add(FootballerEntity footballer);
        Task<List<FootballerEntity>> All();
        Task Delete(Guid id);
        Task<FootballerEntity?> GetById(Guid id);
        Task<List<FootballerEntity>> GetByPage(int page, int pageSize);
        Task Update(FootballerEntity updatedFootballer);
    }
}