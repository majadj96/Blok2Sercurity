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




            string srvCertCN = "SysLog";
            NetTcpBinding binding1 = new NetTcpBinding();
            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:50002/Log"), new X509CertificateEndpointIdentity(srvCert));

            using (MakeSyslogClient proxy = new MakeSyslogClient(binding1, address1))
            {
                Console.WriteLine("Kazem serveru da se loguje " + DateTime.Now.ToString("hh.mm.ss.ffffff"));
                proxy.Logging();

            }











            Console.ReadLine();

            host.Close();


        }
    }
}
