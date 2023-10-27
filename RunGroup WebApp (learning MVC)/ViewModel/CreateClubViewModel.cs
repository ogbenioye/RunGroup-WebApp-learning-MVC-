using RunGroup_WebApp__learning_MVC_.Data.Enum;
using RunGroup_WebApp__learning_MVC_.Models;

namespace RunGroup_WebApp__learning_MVC_.ViewModel
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
