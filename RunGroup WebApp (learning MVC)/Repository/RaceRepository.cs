using Microsoft.EntityFrameworkCore;
using RunGroup_WebApp__learning_MVC_.Data;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Models;

namespace RunGroup_WebApp__learning_MVC_.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext c;
        public RaceRepository(ApplicationDbContext context) 
        {
            c = context;
        }

        public bool Add(Race race)
        {
            c.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            c.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await c.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await c.Races.Include(a => a.Address).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await c.Races.Where(r => r.Address.City == city).ToListAsync();
        }

        public bool Save()
        {
            var saved = c.SaveChanges();
            return saved > 0;
        }

        public bool Update(Race race)
        {
            c.Update(race);
            return Save();
        }
    }
}