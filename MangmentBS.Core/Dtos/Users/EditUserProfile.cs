using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Users
{
    public class EditUserProfile
    {
        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [MaxLength(14, ErrorMessage = "الرقم القومي يجب ان يكون 14 رقم")]
        [MinLength(14, ErrorMessage = "الرقم القومي يجب ان يكون 14 رقم")]
        public string Id { get; set; }
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "كلمة السر تتكون من 6 حروف علي الاقل")]
        [MaxLength(16, ErrorMessage = "كلمة السر لاتتعدي 16 حرف")]
        public string? Password { get; set; }
        [EmailAddress(ErrorMessage = "هذا البريد غير صحيح")]
        public string? Email { get; set; }
        [Phone(ErrorMessage = "هذا الهاتف غير صحيح")]
        [MaxLength(11, ErrorMessage = "رقم الهاتف لا يتعدي 11 رقم")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
