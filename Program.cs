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
            // By Jason Immanuel :: 13550619
            // This application uses MVC Architecture, 
            // Implementation of all doctor, menu, patient functionalities can be found in the /service
            // This implementation also uses CSV for user and appointment data, this can be found in /app_data
            
            // Initialize services
            MenuService menuService = new MenuService();
            UserService userService = new UserService();
            ControllerService controllerService = new ControllerService(
                menuService,
                new DoctorService(menuService, userService),
                new PatientService(menuService, userService),
                new AdminService(menuService, userService)
            );

            // Initial Check
            controllerService.CheckIfLoggedIn();

            Console.ReadLine();
        }
    }
}
