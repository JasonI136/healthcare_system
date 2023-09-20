using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class AdminService : IAdminService
    {
        private readonly IUserService userService;
        private readonly IMenuService menuService;  
        private string userCSVFilePath;
        private string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        // Initiate Constructor
        public AdminService(IMenuService menuService, IUserService userService)
        {
            this.menuService = menuService;
            this.userService = userService;
            this.userCSVFilePath = Path.Combine(projectDirectory, "healthcare_system", "app_data", "user.csv");
        }

        // This function will retrieve the user option and continue it to the appropriate function
        public void processUserInput(int userInput, int UserId)
        {
            switch (userInput)
            {
                case 1:
                    listAllDoctor(UserId);
                    break;
                case 2:
                    checkDoctorDetails(UserId);
                    break;
                case 3:
                    listAllPatients(UserId);
                    break;
                case 4:
                    checkPatientDetails(UserId);
                    break;
                case 5:
                    addDoctor(UserId);
                    break;
                case 6:
                    addPatient(UserId);
                    break;
                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }
        }

        /* Option 1: List All Doctors
        * Lists a shorthand version of every doctor contained in the system to the console
        */
        public void listAllDoctor(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("List All Doctors:");

            // Load and find user
            List<UserDTO> userList = userService.LoadUserList();
            UserDTO user = userList.Find(u => u.UserId == signedInId);
            
            if (user != null)
            {
                List<UserDTO> doctorList = userList.Where(u => string.Equals(u.Role, "Doctor", StringComparison.OrdinalIgnoreCase)).ToList();
                if (doctorList.Count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", "UserId", "Role", "First Name", "Last Name", "Email");
                    Console.WriteLine(new string('-', 90));

                    foreach (var doctor in doctorList)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", doctor.UserId, doctor.Role, doctor.FirstName, doctor.LastName, doctor.Email);
                    }
                }
                else
                {
                    Console.WriteLine("No doctors found in the list.");
                }
            }
            else
            {
                Console.WriteLine("No user found with the ID " + signedInId);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine();
        }

        /* Option 2: Check Doctor Details
        * Prompts the user for an ID and prints the details of the doctor whose ID it belongs to, to the console line by line
        */
        public void checkDoctorDetails(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("Check Doctor Details:");

            // Load and find user
            List<UserDTO> userList = userService.LoadUserList();
            UserDTO user = userList.Find(u => u.UserId == signedInId);
            
            if (user != null)
            {
                Console.WriteLine("Enter the Doctor ID to retrieve details (or press ENTER to exit):");
                string input = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine();
                if (string.IsNullOrEmpty(input))
                {
                    return;
                }
                if (int.TryParse(input, out int doctorId))
                {
                    UserDTO doctor = userList.Find(u => u.UserId == doctorId && string.Equals(u.Role, "Doctor", StringComparison.OrdinalIgnoreCase));
                    if (doctor != null)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", "UserId", "Role", "First Name", "Last Name", "Email");
                        Console.WriteLine(new string('-', 90));
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", doctor.UserId, doctor.Role, doctor.FirstName, doctor.LastName, doctor.Email);
                    }
                    else
                    {
                        Console.WriteLine("No doctor found with the ID " + doctorId);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID entered. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("No user found with the ID " + signedInId);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        /* Option 3: List All Patients
        * Lists a shorthand version of every patient contained in the system to the console
        */
        public void listAllPatients(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("List All Patients:");

            // Load and find user
            List<UserDTO> userList = userService.LoadUserList();
            UserDTO user = userList.Find(u => u.UserId == signedInId);
            
            if (user != null)
            {
                List<UserDTO> patientList = userList.Where(u => string.Equals(u.Role, "Patient", StringComparison.OrdinalIgnoreCase)).ToList();
                if (patientList.Count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", "UserId", "Role", "First Name", "Last Name", "Email");
                    Console.WriteLine(new string('-', 90));

                    foreach (var patient in patientList)
                    {
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", patient.UserId, patient.Role, patient.FirstName, patient.LastName, patient.Email);
                    }
                }
                else
                {
                    Console.WriteLine("No patients found in the list.");
                }
            }
            else
            {
                Console.WriteLine("No user found with the ID " + signedInId);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine();
        }

        /* Option 4: Check Patient Details
        * Prompts the user for an ID and prints the details of the Patient whose ID it belongs to, to the console line by line
        */
        public void checkPatientDetails(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("Check Patient Details:");

            // Load and find user
            List<UserDTO> userList = userService.LoadUserList();
            UserDTO user = userList.Find(u => u.UserId == signedInId);
            
            if (user != null)
            {
                Console.WriteLine("Enter the Patient ID to retrieve details (or press ENTER to exit):");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    return;
                }

                // Parsing input to an integer to check for valid patient ID
                if (int.TryParse(input, out int patientId))
                {
                    // Searching for a patient with the entered ID and a role of 'Patient'
                    UserDTO patient = userList.Find(u => u.UserId == patientId && string.Equals(u.Role, "Patient", StringComparison.OrdinalIgnoreCase));
                    if (patient != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", "UserId", "Role", "First Name", "Last Name", "Email");
                        Console.WriteLine(new string('-', 90));
                        Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", patient.UserId, patient.Role, patient.FirstName, patient.LastName, patient.Email);
                    }
                    else
                    {
                        Console.WriteLine("No patient found with the ID " + patientId);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID entered. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("No user found with the ID " + signedInId);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        /* Option 5: Add New Doctor
        * Prompts the user for all the necessary data needed to generate a new Doctor and add it to the system
        */
        public void addDoctor(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("Add New Doctor:");

            // Load and find user
            List<UserDTO> userList = userService.LoadUserList();
            var doctorList = userList.Where(u => u.Role.ToLower() == "doctor").ToList();

            Console.WriteLine("Please enter the following details to add a new doctor:");

            string firstName;
            while (true)
            {
                Console.Write("First Name: ");
                firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName)) break;
                Console.WriteLine("First name is required.");
            }

            string lastName;
            while (true)
            {
                Console.Write("Last Name: ");
                lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName)) break;
                Console.WriteLine("Last name is required.");
            }

            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email)) break;
                Console.WriteLine("Email is required.");
            }

            string password;
            while (true)
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(password)) break;
                Console.WriteLine("Password is required.");
            }

            string address;
            while (true)
            {
                Console.Write("Address: ");
                address = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(address)) break;
                Console.WriteLine("Address is required.");
            }

            string phone;
            while (true)
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone)) break;
                Console.WriteLine("Phone number is required.");
            }

            Console.Write("Description: ");
            string description = Console.ReadLine();

            // Generate a new user ID for doctor
            int userId = doctorList.Max(u => u.UserId) + 1;

            // Prepare the doctor data to be added to the CSV file
            string doctorData = $"{userId},doctor,{firstName},{lastName},{email},{password},{address},{phone},{description},";

            // Write to user CSV file
            using (StreamWriter sw = File.AppendText(userCSVFilePath))
            {
                sw.Write(Environment.NewLine + doctorData);
            }

            Console.WriteLine();
            Console.WriteLine($"{firstName} {lastName} added successfully");
            Console.WriteLine($"ASSIGNED ID: {userId} ");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        /* Option 6: Add New Patient
        * Prompts the user for all the necessary data needed to generate a new Patient and add it to the system
        */
        public void addPatient(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("Add New Patient:");

            // Load and find user
            List<UserDTO> userList = userService.LoadUserList();
            var doctorList = userList.Where(u => u.Role.ToLower() == "patient").ToList();

            Console.WriteLine("Please enter the following details to add a new patient:");

            string firstName;
            while (true)
            {
                Console.Write("First Name: ");
                firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName)) break;
                Console.WriteLine("First name is required.");
            }

            string lastName;
            while (true)
            {
                Console.Write("Last Name: ");
                lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName)) break;
                Console.WriteLine("Last name is required.");
            }

            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email)) break;
                Console.WriteLine("Email is required.");
            }

            string password;
            while (true)
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(password)) break;
                Console.WriteLine("Password is required.");
            }

            string address;
            while (true)
            {
                Console.Write("Address: ");
                address = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(address)) break;
                Console.WriteLine("Address is required.");
            }

            string phone;
            while (true)
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone)) break;
                Console.WriteLine("Phone number is required.");
            }

            Console.Write("Description: ");
            string description = Console.ReadLine();

            // Generate a new user ID for doctor
            int userId = doctorList.Max(u => u.UserId) + 1;

            // Prepare the doctor data to be added to the CSV file
            string doctorData = $"{userId},patient,{firstName},{lastName},{email},{password},{address},{phone},{description},";

            // Write to user CSV file
            using (StreamWriter sw = File.AppendText(userCSVFilePath))
            {
                sw.Write(Environment.NewLine + doctorData);
            }

            Console.WriteLine();
            Console.WriteLine($"{firstName} {lastName} added successfully");
            Console.WriteLine($"ASSIGNED ID: {userId} ");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
    }
}
