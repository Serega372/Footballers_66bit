using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.Persistence.Repositories
{
    public class FootballersRepository(DatabaseContext databaseContext) : IFootballersRepository
    {
        private readonly DatabaseContext _dbContext = databaseContext;

        public async Task<List<FootballerEntity>> All()
        {
            return await _dbContext.Footballers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<FootballerEntity?> GetById(Guid id)
        {
            return await _dbContext.Footballers
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<FootballerEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.Footballers
                .AsNoTracking()
                .Skip(page * (pageSize - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Add(FootballerEntity footballer)
        {
            footballer.Id = Guid.NewGuid();

            await _dbContext.AddAsync(footballer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(FootballerEntity updatedFootballer)
        {
            _dbContext.Footballers.Update(updatedFootballer);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            await _dbContext.Footballers
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
        }
    }
}
