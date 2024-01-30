using CoreLayer.Entity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BanId { get; set; }
        public int CityId { get; set; }
        public int YearId { get; set; }
        public int FuelId { get; set; }
        public int GearBoxId { get; set; }


        public string OutsideImage { get; set; }
        public string InsideImage { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage ="Bu xana boş qala bilməz")]
        public int DailyPrice { get; set; }
        public bool IsFull { get; set; }
        public bool IsDeactive { get; set; }
        public bool IsPremium { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? PremiumDate { get; set; }


        [NotMapped]
        public IFormFile OutsidePhoto { get; set; }
        [NotMapped]
        public IFormFile InsidePhoto { get; set; }

        public List<CarModel> CarModels { get; set; }
        public AppUser User { get; set;}
        public Ban Ban { get; set; }
        public City City { get; set; }
        public Year Year { get; set; }
        public Fuel Fuel { get; set; }
        public GearBox GearBox { get; set; }
    }
}
