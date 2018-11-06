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

            /// Define the expected service certificate. It is required to establish cmmunication using certificates.
            string srvCertCN = "Servis";
            NetTcpBinding binding1 = new NetTcpBinding();

            ServiceHost host;
            NetTcpBinding binding = new NetTcpBinding();

            string address = "net.tcp://localhost:9998/RBACChange";


            host = new ServiceHost(typeof(RBACChange));
            host.AddServiceEndpoint(typeof(IRBACChange), binding, address);

            host.Open();

            Console.WriteLine("I'm ready for changes 8-)");




            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:9999/Receiver"),
                                      new X509CertificateEndpointIdentity(srvCert));

            using (MakeRBACClient proxy = new MakeRBACClient(binding1, address1))
            {

                /// 1. Communication test
				proxy.UpdateConfiguration();
                Console.WriteLine("Update() finished. Press <enter> to continue ...");
                Console.ReadLine();
            }
           
            host.Close();


        }
    }
}
