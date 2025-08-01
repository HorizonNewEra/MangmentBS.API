﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Entities
{
    public class Client : BaseEntity<string>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public List<Flat> Flats { get; set; } = new List<Flat>();
    }
}
