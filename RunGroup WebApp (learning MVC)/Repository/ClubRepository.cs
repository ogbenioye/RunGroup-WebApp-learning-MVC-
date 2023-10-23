using Microsoft.EntityFrameworkCore;
using RunGroup_WebApp__learning_MVC_.Data;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Models;

namespace RunGroup_WebApp__learning_MVC_.Repository
{
    public class ClubRepository : IClubRepository
    {

        private readonly ApplicationDbContext c;
        public ClubRepository(ApplicationDbContext context) 
        {
            c = context;
        }

        public bool Add(Club club)
        {
            c.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            c.Remove(club);
            return Save();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await c.Clubs.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await c.Clubs.Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await c.Clubs.Where(c => c.Address.City == city).ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await c.Clubs.ToListAsync();   
        }

        public bool Save()
        {
            var saved = c.SaveChanges();
            return saved > 0;
        }

        public bool Update(Club club)
        {
            c.Update(club);
            return Save();
        }
    }
}
