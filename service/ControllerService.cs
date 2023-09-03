using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{

    public class ControllerService : IControllerService
    {
        IMenuService menuService;
        public ControllerService(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public Boolean login()
        {
            // Call Services
            UserService userService = new UserService();

            // Variables
            int user_id;
            string password;

            Console.WriteLine();
            Console.WriteLine("Welcome to the UTS Healthcare System, please input your Credentials:");
            Console.WriteLine();

            Console.Write("User ID: ");
            // Parse the user_id from the console input
            if (!int.TryParse(Console.ReadLine(), out user_id))
            {
                Console.WriteLine("Invalid User ID. Please enter a valid integer.");
                return false;
            }

            Console.Write("Password: ");
            // Read the password from the console input
            password = Console.ReadLine();

            // Call the AuthenticateUser method and store the returned value
            UserDTO authenticatedUser = userService.AuthenticateUser(user_id, password);

            // Check if a user was returned
            if (authenticatedUser != null)
            {
                Console.WriteLine("Valid");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid credentials. Login failed.");
                return false;
            }
        }

        public void loggedIn()
        {
            this.menuService.DoctorMenu();
        }

        public void logout()
        {
            throw new NotImplementedException();
        }

        public void exitSystem()
        {
            throw new NotImplementedException();
        }


    }
}