using Microsoft.Identity.Client;
using RunGroup_WebApp__learning_MVC_.Models;

namespace RunGroup_WebApp__learning_MVC_.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
