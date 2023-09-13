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
            
        }

        public void checkDoctorDetails(int signedInId)
        {

        }

        public void listAllPatients(int signedInId)
        {

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
