using RunGroup_WebApp__learning_MVC_.Data;
using RunGroup_WebApp__learning_MVC_.Interfaces;
using RunGroup_WebApp__learning_MVC_.Models;

namespace RunGroup_WebApp__learning_MVC_.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var clubs = _context.Clubs.Where(c => c.AppUser.Id == curUser.ToString());

            return clubs.ToList();
        }

        public async Task<List<Race>> GetUserRaces()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var races = _context.Races.Where(r => r.AppUser.Id == curUser.ToString());

            return races.ToList();
        }
    }
}
