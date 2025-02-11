using FootballersCatalog.Persistence.Abstract;
using FootballersCatalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballersCatalog.Persistence.Repositories
{
    public class TeamsRepository(DatabaseContext databaseContext) : ITeamsRepository
    {
        public async Task<List<TeamEntity>> All()
        {
            return await databaseContext.Teams
                .AsNoTracking()
                .Include(t => t.Footballers)
                .ToListAsync();
        }

        public async Task<TeamEntity?> GetById(Guid id)
        {
            return await databaseContext.Teams
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TeamEntity> Add(TeamEntity team)
        {
            team.Id = Guid.NewGuid();

            await databaseContext.AddAsync(team);
            await databaseContext.SaveChangesAsync();

            return team;
        }

        public async Task Delete(Guid id)
        {
            var team = await databaseContext.Teams
                .Include(t => t.Footballers)
                .FirstOrDefaultAsync(t => t.Id == id);

            foreach (var footballer in team.Footballers) footballer.TeamTitle = "";
                
            await databaseContext.Teams
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            await databaseContext.SaveChangesAsync();
        }
    }
}
