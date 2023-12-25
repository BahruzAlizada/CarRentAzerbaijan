using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? CompanyDescription { get; set; }
        public string? Address { get; set; }
        public bool IsDeactive { get; set; }
        public bool IsPremium { get; set; }
        public string? UserRole { get; set; }

        public List<Car> Cars { get; set; }
    }
}
