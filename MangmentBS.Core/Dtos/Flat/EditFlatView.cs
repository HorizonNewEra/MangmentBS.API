using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Flat
{
    public class EditFlatView
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        public int? BuildingId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "يجب ادخال رقم الشقة برقم موجب")]
        public int? FlatNumber { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "يجب ادخال السعر المبدئ للشقة برقم موجب")]
        public double? StanderdPrice { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "يجب ادخال رقم الطابق برقم موجب")]
        public int? Floor { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال مساحة الشقة برقم موجب")]
        public double? Size { get; set; }
    }
}
