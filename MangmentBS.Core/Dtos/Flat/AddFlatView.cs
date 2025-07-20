using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Flat
{
    public class AddFlatView
    {
        [Required(ErrorMessage = "Building Id is required.")]
        public int BuildingId { get; set; }
        [Required(ErrorMessage = "رقم الشقة مطلوب")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب ادخال رقم الشقة برقم موجب")]
        public int FlatNumber { get; set; }
        [Required(ErrorMessage = "يجب ادخال السعر المبدئ للشقة")]
        [Range(0.01, double.MaxValue, ErrorMessage = "يجب ادخال السعر المبدئ للشقة برقم موجب")]
        public double StanderdPrice { get; set; }
        [Required(ErrorMessage = "يجب ادخال رقم الطابق")]
        [Range(0, int.MaxValue, ErrorMessage = "يجب ادخال رقم الطابق برقم موجب")]
        public int Floor { get; set; }
        [Required(ErrorMessage = "يجب ادخال مساحة الشقة")]
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال مساحة الشقة برقم موجب")]
        public double Size { get; set; }
    }
}