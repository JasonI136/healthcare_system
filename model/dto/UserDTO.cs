using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.model.dto
{
    // Data Transfer Object for Users
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set;}
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
