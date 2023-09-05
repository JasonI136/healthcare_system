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
        void listPatients();
        void listAppointments();
        void checkPatient();
        void listAppointmentWithPatient();
    }
}
