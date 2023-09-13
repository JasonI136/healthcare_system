using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system
{
    public interface IAdminService
    {
        void processUserInput(int userInput, int UserId);
        void listAllDoctor(int UserId);
        void checkDoctorDetails(int UserId);
        void listAllPatients(int UserId);
        void checkPatientDetails(int UserId);
        void addDoctor(int UserId);
        void addPatient(int UserId);
    }
}

