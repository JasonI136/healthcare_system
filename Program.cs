using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healthcare_system.model.dto;
using healthcare_system.service;
using System.ComponentModel;

namespace healthcare_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize services
            ControllerService controllerService = new ControllerService(new MenuService(), new DoctorService(), new PatientService(), new AdminService());
            MenuService menuService = new MenuService();
            UserService userService = new UserService();
            
            // Initial Check
            controllerService.CheckIfLoggedIn();

            Console.ReadLine();
        }
    }
}
