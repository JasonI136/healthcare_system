using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class DoctorService : IDoctorService
    {
        public void processUserInput(int userInput) 
        {
            Console.Write("HELLO HELLO");
            Console.ReadLine();
        }
        public void DisplayMenu()
        {
            throw new NotImplementedException();
        }
    }
}
