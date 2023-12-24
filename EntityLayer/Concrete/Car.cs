using CoreLayer.Entity;


namespace EntityLayer.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BanId { get; set; }
        public int MarkaId { get; set; }
        public int ModelId { get; set; }
        public int CityId { get; set; }
        public int YearId { get; set; }
        public int FuelId { get; set; }
        public int GearBoxId { get; set; }


        public string? Description { get; set; }
        public int DailyPrice { get; set; }
        public int WeeklyPrice { get; set; }
        public bool IsNotEmpty { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow.AddHours(4);

        public bool IsPremium { get; set; }
        public DateTime PremiumDate { get; set; }


        public List<CarImage> CarImages { get;set; }
        public AppUser User { get; set;}
        public Ban Ban { get; set; }
        public Marka Marka { get; set; }
        public Model Model { get; set; }
        public City City { get; set; }
        public Year Year { get; set; }
        public Fuel Fuel { get; set; }
        public GearBox GearBox { get; set; }
    }
}
