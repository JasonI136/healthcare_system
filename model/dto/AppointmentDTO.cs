using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.model.dto
{
    // Data Transfer Object for Appointments
    public class AppointmentDTO
    {
        public DoctorDTO doctor { get; set; }
        public PatientDTO patient { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
    }
}
