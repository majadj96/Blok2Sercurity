using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutorizationManagerForRBAC
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost host;
            NetTcpBinding binding = new NetTcpBinding();

            string address = "net.tcp://localhost:9998/RBACChange";
            
            host = new ServiceHost(typeof(RBACChange));
            host.AddServiceEndpoint(typeof(IRBACChange), binding, address);
            host.Open();
            Console.WriteLine("I'm ready for changes 8-)");


            ServiceHost hostService;
            NetTcpBinding bindingService = new NetTcpBinding();



            string addressService = "net.tcp://localhost:50010/RBACChange";

            hostService = new ServiceHost(typeof(RBACChange));
            hostService.AddServiceEndpoint(typeof(IRBACChange), binding, address);
            hostService.Open();
            Console.WriteLine("I'm ready for servers 8-)");


            

            

            Console.ReadLine();
            hostService.Close();
            host.Close();


        }
    }
}
