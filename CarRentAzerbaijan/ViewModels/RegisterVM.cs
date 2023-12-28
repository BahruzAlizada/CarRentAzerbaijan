using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentAzerbaijan.ViewModels
{
    public class RegisterVM
    {
        [NotMapped]
        public IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email adresini düzgün şəkildə qeyd edin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifrəni düzgün daxil edin")]
        public string CheckPassword { get; set; }
        public bool IsRemember { get; set; }
    }
}
