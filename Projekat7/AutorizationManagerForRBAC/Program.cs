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
        public static MakeSyslogClient proxyLog;
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

            string addressService = "net.tcp://localhost:50010/Sync";

            hostService = new ServiceHost(typeof(Sync));
            hostService.AddServiceEndpoint(typeof(ISync), bindingService, addressService);
            hostService.Open();
            Console.WriteLine("I'm ready for servers 8-)");


            string srvCertCN = "SysLog";
            NetTcpBinding binding1 = new NetTcpBinding();
            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            Console.WriteLine("Unesite ip za SysLog");
            string add1 = Console.ReadLine();

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);

           
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://" + add1 + ":50002/Log"), new X509CertificateEndpointIdentity(srvCert));
            //EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:50002/Log"), new X509CertificateEndpointIdentity(srvCert));

            Console.WriteLine("Server is going to log " + DateTime.Now.ToString("hh.mm.ss.ffffff"));
            string usrname = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            binding1.CloseTimeout = TimeSpan.MaxValue;
            binding1.OpenTimeout = TimeSpan.MaxValue;
            binding1.ReceiveTimeout = TimeSpan.MaxValue;
            binding1.SendTimeout = TimeSpan.MaxValue;

            proxyLog = new MakeSyslogClient(binding1, address1);


            Console.ReadLine();
            hostService.Close();
            host.Close();


        }
    }
}
