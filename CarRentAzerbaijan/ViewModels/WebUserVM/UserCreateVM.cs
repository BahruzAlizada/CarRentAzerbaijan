using System.ComponentModel.DataAnnotations;

namespace CarRentAzerbaijan.ViewModels.WebUserVM
{
    public class UserCreateVM
    {
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
    }
}
