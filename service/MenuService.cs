using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system.service
{
    public class MenuService : IMenuService
    {
        // This will create the console header box with the page title
        public void DisplayHeader(string page)
        {
            Console.Clear();

            string header = "Health Care System v1";
            int boxWidth = 50;
            string horizontalBorder = new string('─', boxWidth);
            string divider = new string('=', boxWidth);

            // Display top border
            Console.WriteLine("┌" + horizontalBorder + "┐");

            // Display header with padding
            int headerPadding = (boxWidth - header.Length) / 2;
            Console.WriteLine("│" + header.PadLeft(header.Length + headerPadding).PadRight(boxWidth) + "│");

            // Display divider
            Console.WriteLine("├" + divider + "┤");

            // Display page name with padding
            int pagePadding = (boxWidth - page.Length) / 2;
            Console.WriteLine("│" + page.PadLeft(page.Length + pagePadding).PadRight(boxWidth) + "│");

            // Display bottom border
            Console.WriteLine("└" + horizontalBorder + "┘");
        }

        public void DisplayMenu()
        {
            throw new NotImplementedException();
        }

        public void PatientMenu()
        {
            Console.Clear();
            DisplayHeader("Patient Main Menu");

            // List All Patient Options
            Console.WriteLine("Welcome to Hospital Management System: ");
            Console.WriteLine();

            Console.WriteLine("Please Select an Option:");
            Console.WriteLine("======== READ ========");
            Console.WriteLine("1. List Patient Details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");

            Console.WriteLine("======== CREATE ========");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine();

            Console.WriteLine("======== EXIT ========");
            Console.WriteLine("9. Exit to Login");
            Console.WriteLine("0. Exit System");
            Console.WriteLine();
        }
        public void DoctorMenu()
        {
            Console.Clear();
            DisplayHeader("Doctor Main Menu");

            // List All Doctor Options
            Console.WriteLine("Welcome to Hospital Management System");
            Console.WriteLine();

            Console.WriteLine("Please Select an Option:");
            Console.WriteLine("======== READ ========");
            Console.WriteLine("1. List Doctor Details");
            Console.WriteLine("2. List Patients");
            Console.WriteLine("3. List My Appointments");
            Console.WriteLine("4. Check Particular Patient");
            Console.WriteLine("5. List Appointments with patient");
            Console.WriteLine();
            
            Console.WriteLine("======== EXIT ========");
            Console.WriteLine("9. Exit to Login");
            Console.WriteLine("0. Exit System");
            Console.WriteLine();
   
        }
        
        public void AdminMenu()
        {
            Console.Clear();
            DisplayHeader("⚠︎ Admin Menu");

            // List All Admin Options
            Console.WriteLine("ADMIN MENU");
            Console.WriteLine();

            Console.WriteLine("Please Select an Option:");
            Console.WriteLine("======== VIEW USERS ========");
            Console.WriteLine("1. List all Doctors");
            Console.WriteLine("2. Check Doctor Details");
            Console.WriteLine("3. List all Patients");
            Console.WriteLine("4. Check Patient Details");
            Console.WriteLine();
            
            Console.WriteLine("======== ADD USERS ========");
            Console.WriteLine("5. Add New Doctor");
            Console.WriteLine("6. Add New Patient");
            Console.WriteLine();
            
            Console.WriteLine("======== EXIT ========");
            Console.WriteLine("9. Exit to Login");
            Console.WriteLine("0. Exit System");
            Console.WriteLine();
        }


    }
}

