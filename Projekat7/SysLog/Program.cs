using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace SysLog
{
    class Program
    {
        static ServiceHost CreateHostForLog()
        {

           Log l = new Log();
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:50002/Log";

            ServiceHost hostForLog = new ServiceHost(typeof(Log));
            hostForLog.AddServiceEndpoint(typeof(ILog), binding, address);
            hostForLog.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            hostForLog.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
            hostForLog.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            hostForLog.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);


            hostForLog.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            hostForLog.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });



            try
            {
                hostForLog.Open();
                Console.WriteLine("Service for log host is started.");
                return hostForLog;
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
            ServiceHost hostForLog = CreateHostForLog();
            
            Console.ReadLine();

            hostForLog.Close();


        }
    }
}
