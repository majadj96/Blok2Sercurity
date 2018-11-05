using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class HostProtection
    {

        ServiceHost host;

        public HostProtection(string mode)
        {
            NetTcpBinding binding = new NetTcpBinding();


            if (mode.Equals("t"))
            {
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                Console.WriteLine("TRANSPORT");
            }
            else if (mode.Equals("m"))
            {
                binding.Security.Mode = SecurityMode.Message;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                Console.WriteLine("MESSAGE");

            }


            string address = "net.tcp://localhost:9999/FileService";
            

            host = new ServiceHost(typeof(FileService));
            host.AddServiceEndpoint(typeof(IFileService), binding, address);

            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });


            //host.Authorization.ServiceAuthorizationManager = new MyAuthorizationManager();

            //List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            //policies.Add(new CustomAuthorizationPolicy());
            //host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            //host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
        




        }


        public void Open()
        {
            host.Open();
            Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();
        }

        public void Close()
        {
            host.Close();
        }

    }
}
