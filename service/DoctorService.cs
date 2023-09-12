using healthcare_system.model.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class DoctorService : IDoctorService
    {
        private readonly IUserService userService;
        private readonly IMenuService menuService;
        private string appointmentsCSVFile;
        private string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        public DoctorService(IMenuService menuService, IUserService userService)
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
                    listDoctorDetails(UserId);
                    break;
                case 2:
                    listPatients(UserId);
                    break;
                case 3:
                    listAppointments(UserId);
                    break;
                case 4:
                    checkPatient(UserId);
                    break;
                case 5:
                    listAppointmentWithPatient(UserId);
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
        public void listPatients(int signedInId)
        {
            Console.Clear();

            // Load the list of users from the user service
            List<UserDTO> userList = userService.LoadUserList();
            DoctorDTO currentDoctor = new DoctorDTO() { user = userList.Find(u => u.UserId == signedInId) };

            menuService.DisplayHeader($"Listing Patients Appointed to: {currentDoctor.user.FirstName} {currentDoctor.user.LastName}");

            List<AppointmentDTO> appointmentsList = LoadAppointmentsList(signedInId, userList);

            // Display table header
            Console.WriteLine("{0,-30} {1,-30} {2,-30} {3,-15}", "Patient Full Name", "Email Address", "Phone Number", "Address");
            Console.WriteLine(new string('-', 155));

            foreach (var appointment in appointmentsList)
            {
                string patientFullName = $"{appointment.patient.user.FirstName} {appointment.patient.user.LastName}";
                Console.WriteLine("{0,-30} {1,-30} {2,-30} {3,-15}",
                                  patientFullName,
                                  appointment.patient.user.Email,
                                  appointment.patient.user.Phone,
                                  appointment.patient.user.Address);
            }

            Console.ReadLine();
        }

        /* Option 3: List Appointments
        * Lists every appointment involving the currently logged in doctor, regardless of which patient is involved, to the console
        */
        public void listAppointments(int signedInId)
        {
            Console.Clear();

            // Load the list of users from the user service
            List<UserDTO> userList = userService.LoadUserList();
            DoctorDTO currentDoctor = new DoctorDTO() { user = userList.Find(u => u.UserId == signedInId) };

            menuService.DisplayHeader($"Listing Appointments: {currentDoctor.user.FirstName} {currentDoctor.user.LastName}");

            List<AppointmentDTO> appointmentsList = LoadAppointmentsList(signedInId, userList);

            // Display table header
            Console.WriteLine("{0,-30} {1,-30} {2,-50}", "Appointment Date", "Patient Full Name", "Appointment Description");
            Console.WriteLine(new string('-', 110));

            foreach (var appointment in appointmentsList)
            {
                string patientFullName = $"{appointment.patient.user.FirstName} {appointment.patient.user.LastName}";

                Console.WriteLine("{0,-30} {1,-30} {2,-50}",
                                  appointment.date.ToString("dd/MM/yyyy"),
                                  patientFullName,
                                  appointment.description);
            }

            Console.ReadLine();
        }

        /* Option 4: Check Patient
        * Prompts the user for an ID and prints the details of the patient whose ID it belongs to, to the console line by line
        */
        public void checkPatient(int signedInId)
        {
            while (true)
            {
                Console.Clear();

                // Load the list of users from the user service
                List<UserDTO> userList = userService.LoadUserList();
                DoctorDTO currentDoctor = new DoctorDTO() { user = userList.Find(u => u.UserId == signedInId) };

                menuService.DisplayHeader($"Checking Patient: {currentDoctor.user.FirstName} {currentDoctor.user.LastName}");

                // Ask the user to input a patient ID
                Console.Write("Please enter the patient ID (press Enter to exit): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                if (int.TryParse(input, out int patientId))
                {
                    // Find the patient with the input ID
                    UserDTO patient = userList.Find(u => u.UserId == patientId);

                    if (patient != null)
                    {
                        if (patient.Role.ToLower() == "patient")
                        {
                            // Display table header
                            Console.WriteLine("{0,-30} {1,-30} {2,-30} {3,-15}", "Patient Full Name", "Email Address", "Phone Number", "Address");
                            Console.WriteLine(new string('-', 105));

                            string patientFullName = $"{patient.FirstName} {patient.LastName}";
                            Console.WriteLine("{0,-30} {1,-30} {2,-30} {3,-15}",
                                patientFullName,
                                patient.Email,
                                patient.Phone,
                                patient.Address);
                            Console.WriteLine("\nPress Enter to return to the menu.");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid patient ID. The ID does not belong to a patient. Press Enter to try again.");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No patient found with the given ID. Press Enter to try again.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid patient ID. Press Enter to try again.");
                    Console.ReadLine();
                }
            }
        }

        /* Option 5: List Appointments w/ Patient
        * Prompts the user for an ID and finds the patient with that ID
        */
        public void listAppointmentWithPatient(int signedInId)
        {
            while (true)
            {
                Console.Clear();

                // Load the list of users from the user service
                List<UserDTO> userList = userService.LoadUserList();
                DoctorDTO currentDoctor = new DoctorDTO() { user = userList.Find(u => u.UserId == signedInId) };

                menuService.DisplayHeader($"Listing Appointments with Patient: {currentDoctor.user.FirstName} {currentDoctor.user.LastName}");

                // Ask the user to input a patient ID
                Console.Write("Please enter the patient ID (press Enter to exit): ");
                string input = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return;
                }

                if (int.TryParse(input, out int patientId))
                {
                    // Find the patient with the input ID
                    UserDTO patient = userList.Find(u => u.UserId == patientId);

                    if (patient != null && patient.Role.ToLower() == "patient")
                    {
                        List<AppointmentDTO> appointmentsList = LoadAppointmentsList(signedInId, userList);

                        // Filtering the appointments for the specific patient
                        var patientAppointments = appointmentsList.Where(a => a.patient.user.UserId == patientId).ToList();

                        if (patientAppointments.Any())
                        {
                            // Display table header
                            Console.WriteLine("{0,-30} {1,-30} {2,-50}", "Appointment Date", "Patient Full Name", "Appointment Description");
                            Console.WriteLine(new string('-', 110));

                            foreach (var appointment in patientAppointments)
                            {
                                string patientFullName = $"{appointment.patient.user.FirstName} {appointment.patient.user.LastName}";

                                Console.WriteLine("{0,-30} {1,-30} {2,-50}",
                                                  appointment.date.ToString("dd/MM/yyyy"),
                                                  patientFullName,
                                                  appointment.description);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No appointments found for the given patient ID.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid patient ID. The ID does not belong to a patient.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid patient ID.");
                }

                Console.WriteLine("\nPress any key to return to the menu.");
                Console.ReadKey();
            }
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

                    if (signedInId == int.Parse(fields[0]))
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
