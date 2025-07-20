using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Client
{
    public class EditClientView
    {
        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [MaxLength(14, ErrorMessage = "الرقم القومي يجب ان يكون 14 رقم")]
        [MinLength(14, ErrorMessage = "الرقم القومي يجب ان يكون 14 رقم")]
        public string Id { get; set; }
        [StringLength(400, ErrorMessage = "الاسم لا يتخطي 400 حرف")]
        public string? Name { get; set; }
        [MaxLength(11, ErrorMessage = "رقم الهاتف يجب ان يكون 11 رقم")]
        public string? PhoneNumber { get; set; }
        [StringLength(700, ErrorMessage = "العنوان لا يتخطي 700 حرف")]
        public string? Address { get; set; }
    }
}
