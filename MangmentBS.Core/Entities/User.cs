using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Entities
{
    public class User : BaseEntity<string>
    {
        [Required]
        public string Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [Required]
        public string Role { get; set; }
        public Roles Roles { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
