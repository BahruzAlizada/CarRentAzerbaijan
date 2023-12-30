namespace EntityLayer.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; }
        public string? CompanyDescription { get; set; }
        public string? Address { get; set; }
        public bool IsDeactive { get; set; }
        public bool IsPremium { get; set; }

        public int CarCount { get; set; }
        public int PremiumCarCount { get; set; }
    }
}
