using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Building
{
    public class EditBuildingView
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        [StringLength(400, ErrorMessage = "اسم العقار لا يتعدي 400 حرف")]
        public string? Name { get; set; }
        public string? Location { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال مساحة العقار برقم موجب")]
        public double? Size { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال سعر الارض برقم موجب")]
        public double? LandPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال سعر البناء برقم موجب")]
        public double? ConstractionFees { get; set; }
        public string? Description { get; set; }
    }
}
