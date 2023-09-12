using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class PatientService : IPatientService
    {
        private readonly IUserService userService;
        private readonly IMenuService menuService;
        private string appointmentsCSVFile;
        private string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        // Initiate Constructor
        public PatientService(IMenuService menuService, IUserService userService)
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
                    listPatientDetails(UserId);
                    break;
                case 2:
                    listDoctorDetails(UserId);
                    break;
                case 3:
                    listAllAppointments(UserId);
                    break;
                case 4:
                    bookAppointment(UserId);
                    break;
                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }
        }

        /* Option 1: List Patient Details
        * Lists all the fields of the currently logged in patient to the console.
        */
        public void listPatientDetails(int signedInId)
        {
            Console.Clear();

            // Load the list of users from the user service
            List<UserDTO> userList = userService.LoadUserList();

            // Find the logged-in patient details using UserId
            UserDTO currentUser = userList.Find(u => u.UserId == signedInId);

            if (currentUser != null && currentUser.Role.ToLower() == "patient")
            {
                menuService.DisplayHeader($"Patient Details: {currentUser.FirstName} {currentUser.LastName}");

                // Display table header
                Console.WriteLine("{0,-20} {1,-50}", "Field", "Value");
                Console.WriteLine(new string('-', 70));

                // Display patient details
                Console.WriteLine("{0,-20} {1,-50}", "Patient ID", currentUser.UserId);
                Console.WriteLine("{0,-20} {1,-50}", "First Name", currentUser.FirstName);
                Console.WriteLine("{0,-20} {1,-50}", "Last Name", currentUser.LastName);
                Console.WriteLine("{0,-20} {1,-50}", "Email", currentUser.Email);
                Console.WriteLine("{0,-20} {1,-50}", "Phone Number", currentUser.Phone);
                Console.WriteLine("{0,-20} {1,-50}", "Address", currentUser.Address);
                Console.WriteLine("{0,-20} {1,-50}", "Role", currentUser.Role);
            }
            else
            {
                Console.WriteLine("Invalid User ID or the user is not a patient.");
            }

            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
        }


        /* Option 2: List My Doctor Details
        * Lists all the fields of the doctor that is registered with the currently logged in patient
        */
        public void listDoctorDetails(int signedInId)
        {
            Console.Clear();

            // Load the list of users from the user service
            List<UserDTO> userList = userService.LoadUserList();

            // Load the appointments list based on the currently signed-in patient ID
            List<AppointmentDTO> appointmentsList = LoadAppointmentsList(signedInId, userList);

            if (appointmentsList.Count > 0)
            {
                // Assuming the first appointment contains the registered doctor details
                DoctorDTO registeredDoctor = appointmentsList[0].doctor;

                if (registeredDoctor != null)
                {
                    menuService.DisplayHeader("My Doctor Details");

                    // Display table header
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("{0,-10} {1,-30} {2,-20} {3,-20} {4,-20} {5,-30}", "UserId", "Full Name", "Email", "Phone", "Address", "Description");
                    Console.WriteLine(new string('-', 120));

                    // Display doctor details in a row
                    Console.WriteLine("{0,-10} {1,-30} {2,-20} {3,-20} {4,-20} {5,-30}",
                                      registeredDoctor.user.UserId,
                                      "Dr " + registeredDoctor.user.FirstName + " " + registeredDoctor.user.LastName,
                                      registeredDoctor.user.Email,
                                      registeredDoctor.user.Phone,
                                      registeredDoctor.user.Address,
                                      registeredDoctor.user.Description);
                }
                else
                {
                    Console.WriteLine("No doctor details found.");
                }
            }
            else
            {
                Console.WriteLine("No appointments found.");
            }

            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
        }





        /* Option 3: List All Appointments
        * Lists the details of all past appointments involving the currently logged in patient
        */
        public void listAllAppointments(int signedInId) 
        {
            Console.Clear();

            // Load the list of users from the user service
            List<UserDTO> userList = userService.LoadUserList();
            DoctorDTO currentDoctor = new DoctorDTO() { user = userList.Find(u => u.UserId == signedInId) };

            menuService.DisplayHeader($"Listing Appointments: {currentDoctor.user.FirstName} {currentDoctor.user.LastName}");

            List<AppointmentDTO> appointmentsList = LoadAppointmentsList(signedInId, userList);

            // Display table header
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("{0,-30} {1,-30} {2,-50}", "Doctor Name", "Patient Name", "Appointment Description");
            Console.WriteLine(new string('-', 110));

            foreach (var appointment in appointmentsList)
            {
                string doctorFullName = $"Dr {appointment.doctor.user.FirstName} {appointment.doctor.user.LastName}";
                string patientFullName = $"{appointment.patient.user.FirstName} {appointment.patient.user.LastName}";

                Console.WriteLine("{0,-30} {1,-30} {2,-50}",
                                  doctorFullName,
                                  patientFullName,
                                  appointment.description);
            }

            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadLine();
        }

        /* Option 4: Book Appointment
        * Prompts the user for all the necessary information to generate a new appointment
        */
        public void bookAppointment(int signedInId)
        {
            Console.Clear();

            // Load the list of users from the user service
            List<UserDTO> userList = userService.LoadUserList();

            // Get the list of all doctors
            var doctorList = userList.Where(u => u.Role.ToLower() == "doctor").ToList();

            if (doctorList.Count == 0)
            {
                Console.WriteLine("No doctors available.");
                return;
            }

            // Display all doctors
            Console.WriteLine("{0,-10} {1,-30} {2,-20}", "Doctor ID", "Doctor Name", "Email");
            Console.WriteLine(new string('-', 60));

            foreach (var doctor in doctorList)
            {
                Console.WriteLine("{0,-10} {1,-30} {2,-20}", doctor.UserId, $"Dr. {doctor.FirstName} {doctor.LastName}", doctor.Email);
            }

            // Ask for doctor ID input
            Console.WriteLine("\nPlease enter the ID number of the doctor you wish to book an appointment with:");
            int doctorIdInput;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out doctorIdInput) && doctorList.Any(d => d.UserId == doctorIdInput))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please try again:");
                }
            }

            // Get current date
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");

            // Prepare the appointment data
            string appointmentData = $"{doctorIdInput},{signedInId},{currentDate},\"Booked Appointment\"";

            // Write to appointments.csv file
            using (StreamWriter sw = File.AppendText(appointmentsCSVFile))
            {
                sw.WriteLine(appointmentData);
            }

            Console.WriteLine("Appointment booked successfully. Press any key to return to the menu.");
            Console.ReadKey();
        }


        /* Function for Loading Appointments List based off currently signed in */
        public List<AppointmentDTO> LoadAppointmentsList(int signedInId, List<UserDTO> userList)
        {
            List<AppointmentDTO> appointmentsList = new List<AppointmentDTO>();

            // Open the appointments CSV file for reading
            using (StreamReader reader = new StreamReader(appointmentsCSVFile))
            {
                // Skip the header row to begin reading data rows
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    if (signedInId == int.Parse(fields[1]))
                    {
                        // Create a new AppointmentDTO object and populate its properties with parsed data
                        AppointmentDTO appointment = new AppointmentDTO
                        {
                            doctor = new DoctorDTO()
                            {
                                user = userList.Find(u => u.UserId == int.Parse(fields[0]))
                            },
                            patient = new PatientDTO
                            {
                                user = userList.Find(u => u.UserId == int.Parse(fields[1]))
                            },
                            date = DateTime.Parse(fields[2]),
                            description = fields[3]
                        };

                        // Add the populated AppointmentDTO object to the appointments list
                        appointmentsList.Add(appointment);
                    }
                }
            }

            return appointmentsList;
        }
    }
}
