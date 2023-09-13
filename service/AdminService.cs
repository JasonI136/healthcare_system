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
        private string appointmentsCSVFile;
        private string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        // Initiate Constructor
        public AdminService(IMenuService menuService, IUserService userService)
        {
            this.menuService = menuService;
            this.userService = userService;
            this.appointmentsCSVFile = Path.Combine(projectDirectory, "healthcare_system", "app_data", "appointments.csv");
        }
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

        public void listAllDoctor(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("List All Doctors:");

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

            Console.ReadLine();
        }



        public void checkDoctorDetails(int signedInId)
        {

        }

        public void listAllPatients(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("List All Patients:");

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

            Console.ReadLine();
        }


        public void checkPatientDetails(int signedInId)
        {

        }

        public void addDoctor(int signedInId)
        {

        }
        public void addPatient(int signedInId)
        {

        }
    }
}
