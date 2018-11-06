using Common;
using System;
using System.Collections.Generic;
using System.Linq;
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

            string address = "net.tcp://localhost:9999/RBACChange";

            host = new ServiceHost(typeof(RBACChange));
            host.AddServiceEndpoint(typeof(IRBACChange), binding, address);

            host.Open();

            Console.WriteLine("I'm ready for changes 8-)");

            Console.ReadLine();
            host.Close();


        }
    }
}
