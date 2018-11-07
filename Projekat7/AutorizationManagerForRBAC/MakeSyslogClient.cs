using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutorizationManagerForRBAC
{
    
        public class MakeSyslogClient : ChannelFactory<ILog>, ILog, IDisposable
        {
            ILog factory;

            public MakeSyslogClient(NetTcpBinding binding, EndpointAddress address)
                : base(binding, address)
            {
                /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
                string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

                this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
                this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
                this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

                /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
                this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

                factory = this.CreateChannel();
            }

       
        public void Logging(string userName)
            {
                try
                {
                    factory.Logging(userName);
                    Console.WriteLine("Logging() allowed");

                }
                catch (CommunicationException comEx)
                {
                    Console.WriteLine("Error while trying to Logging(). {0}", comEx.Message);
                }
            }

            public void LoggingFail(string userName)
            {
                try
                {
                    factory.LoggingFail(userName);
                    Console.WriteLine("LoggingFail() allowed");

                }
                catch (CommunicationException comEx)
                {
                    Console.WriteLine("Error while trying to LoggingFail(). {0}", comEx.Message);
                }
            }
        }
   
}
