using RunGroup_WebApp__learning_MVC_.Data.Enum;
using RunGroup_WebApp__learning_MVC_.Models;

namespace RunGroup_WebApp__learning_MVC_.ViewModel
{
    public class CreateRaceViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
