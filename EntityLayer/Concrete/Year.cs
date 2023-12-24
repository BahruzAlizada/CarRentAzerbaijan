using CoreLayer.Entity;
using System.ComponentModel.DataAnnotations;


namespace EntityLayer.Concrete
{
    public class Year : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public int Yearr { get; set; }
        public bool IsDeactive { get; set; }

        public List<Car> Cars { get; set; }
    }
}
