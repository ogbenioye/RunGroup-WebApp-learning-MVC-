using RunGroup_WebApp__learning_MVC_.Models;

namespace RunGroup_WebApp__learning_MVC_.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<List<Club>> GetUserClubs();
        public Task<List<Race>> GetUserRaces();
    }
}
