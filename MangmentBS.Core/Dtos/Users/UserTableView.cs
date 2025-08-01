﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Users
{
    public class UserTableView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string ?PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

    }
}
