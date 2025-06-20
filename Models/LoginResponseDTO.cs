using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Role { get; set; }
    }
}
