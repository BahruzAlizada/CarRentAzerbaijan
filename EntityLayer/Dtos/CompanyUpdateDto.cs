using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Dtos
{
    public class CompanyUpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Düzgün Email adresi qeyd edin")]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public string? CompanyDescription { get; set; }
        public string? Address { get; set; }
    }
}
