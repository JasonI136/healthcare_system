using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserService userService;
        private readonly IMenuService menuService;

        public DoctorService(IMenuService menuService, IUserService userService)
        {
            this.menuService = menuService;
            this.userService = userService;
        }

        public void processUserInput(int userInput, int UserId) 
        {
            switch (userInput)
            {
                case 1:
                    listDoctorDetails(UserId);
                    break;
                case 2:
                    listPatients();
                    break;
                case 3:
                    listAppointments();
                    break;
                case 4:
                    checkPatient();
                    break;
                case 5:
                    listAppointmentWithPatient();
                    break;
                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }
        }

        /* Option 1: List Doctor Details
        * Lists the fields of the currently logged in doctor to the console.
        */
        public void listDoctorDetails(int signedInId)
        {
            Console.Clear();
            menuService.DisplayHeader("List Doctor Details:");

            List<UserDTO> userList = userService.LoadUserList();

            UserDTO user = userList.Find(u => u.UserId == signedInId);
            if (user != null)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", "UserId", "Role", "First Name", "Last Name", "Email");
                Console.WriteLine(new string('-', 90));
                Console.WriteLine("{0,-10} {1,-10} {2,-20} {3,-20} {4,-30}", user.UserId, user.Role, user.FirstName, user.LastName, user.Email);
            }
            else
            {
                Console.WriteLine("No user found with the ID " + signedInId);
            }

            Console.ReadLine();
        }


        /* Option 2: List Patients
        * Lists a shorthand outline of every patient that is registered with the currently logged in doctor to the console
        */
        public void listPatients()
        {

        }

        /* Option 3: List Appointments
        * Lists every appointment involving the currently logged in doctor, regardless of which patient is involved, to the console
        */
        public void listAppointments()
        {

        }

        /* Option 4: Check Patient
        * Prompts the user for an ID and prints the details of the patient whose ID it belongs to, to the console line by line
        */
        public void checkPatient()
        {

        }

        /* Option 5: List Appointments w/ Patient
        * Prompts the user for an ID and finds the patient with that ID
        */
        public void listAppointmentWithPatient()
        {

        }

    }
}
