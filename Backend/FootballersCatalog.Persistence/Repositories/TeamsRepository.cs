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
    public class TeamsRepository(DatabaseContext databaseContext) : ITeamsRepository
    {
        private readonly DatabaseContext _dbContext = databaseContext;

        public async Task<List<TeamEntity>> All()
        {
            return await _dbContext.Teams
                .AsNoTracking()
                .Include(t => t.Footballers)
                .ToListAsync();
        }

        public async Task<TeamEntity?> GetById(Guid id)
        {
            return await _dbContext.Teams
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TeamEntity> Add(TeamEntity team)
        {
            team.Id = Guid.NewGuid();

            await _dbContext.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            return team;
        }

        public async Task Delete(Guid id)
        {
            var team = await _dbContext.Teams
                .Include(t => t.Footballers)
                .FirstOrDefaultAsync(t => t.Id == id);

            foreach (var footballer in team.Footballers) footballer.TeamTitle = "";
                
            await _dbContext.Teams
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
        }
    }
}
