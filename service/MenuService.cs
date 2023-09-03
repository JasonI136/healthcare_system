using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class MenuService : IMenuService
    {
        
        public void DisplayMenu()
        {
            throw new NotImplementedException();
        }

        public void PatientMenu()
        {
            // List All Patient Options
            Console.WriteLine("1. List Patient Details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            
            Console.WriteLine("9. Exit to Login");
            Console.WriteLine("0. Exit System");

        }
        public void DoctorMenu()
        {
            Console.Clear();
            // List All Patient Options
            Console.WriteLine("1. List Doctor Details");
            Console.WriteLine("2. List Patients");
            Console.WriteLine("3. List My Appointments");
            Console.WriteLine("4. Check Particular Patient");
            Console.WriteLine("5. List appointments with patient");
            
            Console.WriteLine("9. Exit to Login");
            Console.WriteLine("0. Exit System");

            
        }
        public void AdminMenu()
        {
            throw new NotImplementedException();
        }



        
    }
}

