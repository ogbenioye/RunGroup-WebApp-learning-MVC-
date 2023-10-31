namespace RunGroup_WebApp__learning_MVC_.ViewModel
{
    public class EditUserProfileViewModel
    {
        public int? Pace { get; set; }
        public int? Mielage { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
