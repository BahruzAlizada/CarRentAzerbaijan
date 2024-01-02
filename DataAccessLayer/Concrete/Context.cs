using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OK3QKVJ;Database=CarRentAzerbaijan;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;Integrated Security=True;");
        }



        public DbSet<Ban> Bans { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<GearBox> GearBoxes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModels> CarModels { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
    }




}
