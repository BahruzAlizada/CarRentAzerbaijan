using System.ComponentModel.DataAnnotations;

namespace CarRentAzerbaijan.ViewModels.WebUserVM
{
    public class UserVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email adresini düzgün şəkildə qeyd edin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string Name { get; set; }
        public string UserRole { get; set; }
        public bool IsDeactive { get; set; }
    }
}
