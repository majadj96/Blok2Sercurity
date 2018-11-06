﻿using SecurityManager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Choose 't' for Transport Mode or 'm' for Message Mode");
            string mode = Console.ReadLine();

            HostProtection hostProtection = new HostProtection(mode);

            hostProtection.Open();



            hostProtection.Close();


        }
    }
}
