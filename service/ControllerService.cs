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
        // Initialize DTO
        UserDTO authenticatedUser;

        // Initialize services
        IMenuService menuService;
        IDoctorService doctorService;
        IPatientService patientService;
        IAdminService adminService;

        // Initiate Constructor
        public ControllerService(IMenuService menuService, IDoctorService doctorService, IPatientService patientService, IAdminService adminService)
        {
            this.menuService = menuService;
            this.doctorService = doctorService;
            this.patientService = patientService;
            this.adminService = adminService;
        }

        public void CheckIfLoggedIn()
        {
            Boolean isUserLoggedIn = false;

            while (true) // Infinite loop to keep asking for login until successful
            {
                // This will check if user is logged in, if isUserLoggedIn is FALSE; execute the login function
                // HOWEVER: if isUserLoggedIn is TRUE, early return and do not execute login function
                isUserLoggedIn = isUserLoggedIn || login(); 

                if (isUserLoggedIn)
                {
                    returnToMenu();
                }
                else
                {
                    Console.WriteLine("Would you like to try again? (y/n): ");
                    string response = Console.ReadLine();

                    if (response.ToLower() != "n")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Exiting...");
                        break; // Break out of the loop if the user chooses not to try again
                    }
                }
            }
        }
        public Boolean login()
        {
            // Call Services
            this.menuService.DisplayHeader("Login Page");
            UserService userService = new UserService();

            int user_id;
            string password = "";

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
            // Read the password from the console input but display asterisks
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                // BACKSPACE and ENTER Should not work 
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();

            // Call the AuthenticateUser method and store the returned value
            authenticatedUser = userService.AuthenticateUser(user_id, password);

            // Check if a user was returned
            if (authenticatedUser != null)
            {
                Console.WriteLine();
                Console.WriteLine("Successful Login Welcome, " + authenticatedUser.FirstName +  " " + authenticatedUser.LastName);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("Invalid credentials. Login failed.");
                return false;
            }
        }

        // returnToMenu() handles the menu navigation for all users
        public void returnToMenu()
        {
            int userOption;
            
            // For each case, it will
            // 1. Display the Menu for the role
            // 2. Get the input from the user
            // 3. Pass the user input to the roles controller/service
            switch (authenticatedUser.Role.ToLower())
            {
                case "doctor":
                    menuService.DoctorMenu();
                    Console.WriteLine("Logged in as: " + authenticatedUser.FirstName + " " + authenticatedUser.LastName);
                    Console.WriteLine();
                    Console.WriteLine();
                    userOption = getMenuOption();
                    doctorService.processUserInput(userOption, authenticatedUser.UserId);
                    break;
                case "patient":
                    menuService.PatientMenu();
                    Console.WriteLine("Logged in as: " + authenticatedUser.FirstName + " " + authenticatedUser.LastName);
                    Console.WriteLine();
                    Console.WriteLine();
                    userOption = getMenuOption();
                    patientService.processUserInput(userOption, authenticatedUser.UserId);
                    break;
                case "admin":
                    menuService.AdminMenu();
                    Console.WriteLine("Logged in as: " + authenticatedUser.FirstName + " " + authenticatedUser.LastName);
                    Console.WriteLine();
                    Console.WriteLine();
                    userOption = getMenuOption();
                    adminService.processUserInput(userOption, authenticatedUser.UserId);
                    break;
                default:
                    Console.WriteLine("Invalid Role");
                    break;
            }
        }

        // getMenuOption() handles the option when in the main menu after being signed in
        public int getMenuOption()
        {
            int menuOption;

            while (true)
            {
                Console.Write("User Input: ");
                string userInput = Console.ReadLine();

                bool isInteger = int.TryParse(userInput, out menuOption);

                switch (isInteger)
                {
                    // OPTION 9: Logout
                    case true when menuOption == 9:
                        logout();
                        return -1;

                    // OPTION 0: Exit the entire application
                    case true when menuOption == 0:
                        Environment.Exit(0);
                        return -1; 

                    case true:
                        Console.WriteLine("You entered a valid integer: " + menuOption);
                        return menuOption;

                    default:
                        Console.Write("Invalid input. Press any key to retry.");
                        Console.ReadLine();
                        returnToMenu();
                        break;
                }
            }
        }

        public void logout()
        {
            // If authenticatedUser has a role assigned
            if (!string.IsNullOrEmpty(authenticatedUser.Role))
            {
                authenticatedUser = null; // Set to null to wipe currently authenticated user
                Console.WriteLine("You have Logged out successfully.");
                Console.ReadLine();
                CheckIfLoggedIn(); // Call CheckIfLoggedIn function
            }
        }
    }
}