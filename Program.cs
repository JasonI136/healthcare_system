using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healthcare_system.model.dto;
using healthcare_system.service;

namespace healthcare_system
{
    internal class Program
    {

        //static void DrawBox(int x, int y, int width, int height)
        //{
        //    Console.SetCursorPosition(x, y);
        //    Console.Write("┌" + new string('─', width) + "┐");

        //    for (int i = 1; i <= height; i++)
        //    {
        //        Console.SetCursorPosition(x, y + i);
        //        Console.Write("│" + new string(' ', width) + "│");
        //    }

        //    Console.SetCursorPosition(x, y + height + 1);
        //    Console.Write("└" + new string('─', width) + "┘");
        //}


        static void Main(string[] args)
        {
            ControllerService controllerService = new ControllerService(new MenuService());
            if (controllerService.login()){
                controllerService.loggedIn();
            }
            

            //DrawBox(0, 0, 20, 10);
            //Console.ReadLine();


            Console.ReadLine();
        }
    }
}
