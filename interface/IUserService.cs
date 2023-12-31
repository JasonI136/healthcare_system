﻿using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system
{
    public interface IUserService
    {
        // Interface for User Services
        UserDTO AuthenticateUser(int userId, string password);
        List<UserDTO> LoadUserList();
    }
}

