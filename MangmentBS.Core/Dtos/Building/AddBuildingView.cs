using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Building
{
    public class AddBuildingView
    {
        [Required(ErrorMessage = "اسم العقار مطلوم")]
        [StringLength(400, ErrorMessage = "اسم العقار لا يتعدي 400 حرف")]
        public string Name { get; set; }
        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Location { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال مساحة العقار برقم موجب")]
        [Required(ErrorMessage = "مساحة العقار مطلوب")]
        public double Size { get; set; }
        [Required(ErrorMessage = "سعر الارض مطلوب")]
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال سعر الارض برقم موجب")]
        public double LandPrice { get; set; }
        [Required(ErrorMessage = "مصاريف البناء مطلوب")]
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال سعر البناء برقم موجب")]
        public double ConstractionFees { get; set; }
        public string? Description { get; set; }
    }
}
