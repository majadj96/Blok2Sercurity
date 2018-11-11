using Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        public static RBACProxy proxy;

        static ServiceHost CreateHostForRBAC(string port)
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;


            //ELENA 9.11.
           
            
            string address = "net.tcp://localhost:" + port + "/UpdateConfig";

           
            ServiceHost hostForRBAC = new ServiceHost(typeof(UpdateConfig));
            hostForRBAC.AddServiceEndpoint(typeof(IUpdateConfig), binding, address);
            hostForRBAC.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            hostForRBAC.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
            hostForRBAC.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            hostForRBAC.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            return hostForRBAC;
        }

        public static void UpdateDictionary()
        {
            InMemoryCash.GroupsAndPermissionsDictionary = proxy.SetDictionary();
        }


        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            Console.WriteLine("Unesite IP adresu RBAC servera:");
            string ip = Console.ReadLine();
            string address = "net.tcp://"+ip+":50010/Sync";

            proxy = new RBACProxy(binding, address);
            while (!proxy.isAlive())
            {
                Thread.Sleep(1000);
                proxy = new RBACProxy(binding, address);
            }
            InMemoryCash.GroupsAndPermissionsDictionary = proxy.SetDictionary();

            Console.WriteLine("Choose 't' for Transport Mode or 'm' for Message Mode..");
            string mode = Console.ReadLine();
            HostProtection hostProtection = new HostProtection(mode);


            
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            string port = proxy.GetPort(IP4Address);
            
            ServiceHost hostForRBAC = CreateHostForRBAC(port);
           
            hostForRBAC.Open();
            hostProtection.Open(mode);
            
            Console.ReadLine();
            hostProtection.Close();
            hostForRBAC.Close();
            
        }

      
    }
}
