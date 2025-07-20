using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Users
{
    public class LogInView
    {
        [Required(ErrorMessage = "يرجي ادخال اسم المستخدم")]
        [MaxLength(50, ErrorMessage = "اسم المستخدم لا يتعدي 50 حرف")]
        [MinLength(3, ErrorMessage = "اسم المستخدم يتكون من 3 حروف علي الاقل")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "يرجي ادخال كلمة السر")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "كلمة السر تتكون من 6 حروف علي الاقل")]
        [MaxLength(16, ErrorMessage = "كلمة السر لاتتعدي 16 حرف")]
        public string Password { get; set; }
    }
}
