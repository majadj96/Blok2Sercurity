using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SysLog
{
    class Program
    {
        static ServiceHost CreateHostForLog()
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:50001/UpdateConfig";

            ServiceHost hostForRBAC = new ServiceHost(typeof(UpdateConfig));
            hostForRBAC.AddServiceEndpoint(typeof(IUpdateConfig), binding, address);
            hostForRBAC.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            hostForRBAC.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
            hostForRBAC.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            hostForRBAC.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            try
            {
                hostForRBAC.Open();
                Console.WriteLine("Service for RBAC host is started.");
                return hostForRBAC;
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] {0}", e.Message);
                //  Console.WriteLine("[StackTrace] {0}", e.StackTrace);
                return null;
            }
        }


        static void Main(string[] args)
        {
        }
    }
}
