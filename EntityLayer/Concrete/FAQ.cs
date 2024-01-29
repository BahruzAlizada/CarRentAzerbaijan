using CoreLayer.Entity;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class FAQ : IEntity
    {
        public int Id { get; set; }
        public int FaqCategoryId { get; set; }

        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Quetsion { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Answer { get; set; }
        public bool IsDeactive { get; set; }

        public FaqCategory FaqCategory { get; set; }
    }
}
