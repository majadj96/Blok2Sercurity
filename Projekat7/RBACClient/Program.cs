﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RBACClient
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/RBACChange";

            using (MakeProxy proxy = new MakeProxy(binding, address))
            {

                proxy.Change();
                Console.ReadLine();
            }


         
        }
    }
}
