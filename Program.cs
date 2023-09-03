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
            ControllerService controllerService = new ControllerService(new MenuService());
            MenuService menuService = new MenuService();
            UserService userService = new UserService();

            menuService.DisplayHeader("Login Page");

            while (true) // Infinite loop to keep asking for login until successful
            {
                if (controllerService.login())
                {
                    controllerService.loggedIn();
                    break; // Break out of the loop if login is successful
                }
                else
                {
                    Console.WriteLine("Would you like to try again? (y/n): ");
                    string response = Console.ReadLine();

                    if (response.ToLower() != "n")
                    {
                        Console.Clear(); // Clear the console
                        menuService.DisplayHeader("Login Page"); // Display the login header
                    }
                    else
                    {
                        Console.WriteLine("Exiting...");
                        break; // Break out of the loop if the user chooses not to try again
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
