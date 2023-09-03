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
        void ListAllUsers();
        List<UserDTO> LoadData(string csvFilePath);

        UserDTO AuthenticateUser(int userId, string password);
    }
}

