﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthcare_system
{
    public interface IMenuService
    {
        void PatientMenu();
        void DoctorMenu();
        void AdminMenu();
    }
}


