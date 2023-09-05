using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system
{
    public interface IUserService
    {
        UserDTO AuthenticateUser(int userId, string password);
        void ListAllUsers();
        List<UserDTO> LoadData();
        List<UserDTO> LoadUserList();

        
    }
}

