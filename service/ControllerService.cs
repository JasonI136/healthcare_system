using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{

    public class ControllerService : IControllerService
    {
        IMenuService menuService;
        public ControllerService(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public Boolean login()
        {
            int user_id;
            string password;

            Console.Write("ID: ");
            Console.ReadLine();
            Console.Write("Password: ");
            Console.ReadLine();

            Console.WriteLine("Valid");
            return true;
        }

        public void loggedIn()
        {
            this.menuService.DoctorMenu();
        }

        public void logout()
        {
            throw new NotImplementedException();
        }

        public void exitSystem()
        {
            throw new NotImplementedException();
        }


    }
}