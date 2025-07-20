using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Users
{
    public class EditUserAdmin
    {
        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        [MaxLength(14, ErrorMessage = "الرقم القومي يجب ان يكون 14 رقم")]
        [MinLength(14, ErrorMessage = "الرقم القومي يجب ان يكون 14 رقم")]
        public string Id { get; set; }
        [Required(ErrorMessage = "يرجي ادخال الوظيفة")]
        public string Role { get; set; }
    }
}
