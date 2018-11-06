using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {

            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:9999/Receiver";
            ServiceHost host = new ServiceHost(typeof(UpdateConfig));
            host.AddServiceEndpoint(typeof(IUpdateConfig), binding, address);





            Console.WriteLine("Choose 't' for Transport Mode or 'm' for Message Mode");
            string mode = Console.ReadLine();

            HostProtection hostProtection = new HostProtection(mode);

            hostProtection.Open();
            hostProtection.Close();


        }
    }
}
