using System.ComponentModel.DataAnnotations;

namespace CarRentAzerbaijan.ViewModels.WebUserVM
{
    public class UserUpdateVM
    {
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email adresini düzgün şəkildə qeyd edin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string Name { get; set; }
    }
}
