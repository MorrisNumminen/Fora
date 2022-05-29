using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Shared
{
    public class UserDto
    {
        [Required]
        [StringLength(5)]
        public string? Username { get; set; }
        [Required]
        [StringLength(4)]
        public string? Password { get; set; }
    }
}
