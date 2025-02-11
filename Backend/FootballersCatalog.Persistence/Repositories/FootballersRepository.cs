using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballersCatalog.Persistence.Repositories
{
    public class FootballersRepository(DatabaseContext databaseContext) : IFootballersRepository
    {
        public async Task<List<FootballerEntity>> All()
        {
            return await databaseContext.Footballers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<FootballerEntity?> GetById(Guid id)
        {
            return await databaseContext.Footballers
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<FootballerEntity>> GetByPage(int page, int pageSize)
        {
            return await databaseContext.Footballers
                .AsNoTracking()
                .Skip(page * (pageSize - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Add(FootballerEntity footballer)
        {
            footballer.Id = Guid.NewGuid();

            await databaseContext.AddAsync(footballer);
            await databaseContext.SaveChangesAsync();
        }

        public async Task Update(FootballerEntity updatedFootballer)
        {
            databaseContext.Footballers.Update(updatedFootballer);

            await databaseContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await databaseContext.Footballers
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();

            await databaseContext.SaveChangesAsync();
        }
    }
}
