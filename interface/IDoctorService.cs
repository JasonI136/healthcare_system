using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system
{
    public interface IDoctorService
    {
        void processUserInput(int userInput, int UserId);
        void listDoctorDetails(int userInput);
        void listPatients(int userInput);
        void listAppointments(int userInput);
        void checkPatient(int userInput);
        void listAppointmentWithPatient(int userInput);
    }
}
