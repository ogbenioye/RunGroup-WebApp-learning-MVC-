using RunGroup_WebApp__learning_MVC_.Data.Enum;
using RunGroup_WebApp__learning_MVC_.Models;
using System.Globalization;

namespace RunGroup_WebApp__learning_MVC_.ViewModel
{
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
