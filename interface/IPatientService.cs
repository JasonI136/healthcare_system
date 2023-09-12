using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system
{
    public interface IPatientService
    {
        void processUserInput(int userInput, int UserId);

        void listPatientDetails(int UserId);
        void listDoctorDetails(int UserId);
        void listAllAppointments(int UserId);
        void bookAppointment();
        
    }
}
