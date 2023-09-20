using healthcare_system.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system
{
    public interface IControllerService
    {
        // Interface for Controller Services
        Boolean login();
        void returnToMenu();
        void CheckIfLoggedIn();
        int getMenuOption();
        void logout();
    }
}