using CoreLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Marka : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Image { get; set; }
        public bool IsDeactive { get; set; }

        public List<Model> Models { get; set; }
        public List<Car> Cars { get; set; }
    }
}
