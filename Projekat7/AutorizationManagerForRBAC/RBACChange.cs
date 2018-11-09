using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutorizationManagerForRBAC
{
    class RBACChange : IRBACChange
    {
        public static List<string> ListOfServers = new List<string>();

        public void Change(Dictionary<string, List<string>> GroupsAndPermissionsDict)
        {
            ResXResourceWriter writer = new ResXResourceWriter("..\\..\\GroupsAndPermisions.resx");
            string permisije = string.Empty;



            foreach(string grupa in GroupsAndPermissionsDict.Keys)
            {

                foreach(string permisija in GroupsAndPermissionsDict[grupa])
                {
                    permisije += permisija + ",";

                }
                permisije = permisije.Substring(0, permisije.Length - 1);
                writer.AddResource(grupa, permisije);
                permisije = string.Empty;
            }

            writer.Generate();
            writer.Close();
            
            
            string srvCertCN1 = "Servis";
            NetTcpBinding binding2 = new NetTcpBinding();
            binding2.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;


            //ELENA 9.11.
            Console.WriteLine("Unesite port za KanalKaServisu:");
            string port = Console.ReadLine();
            Console.WriteLine("Unesite ipadresu za KanalKaServisu:");
            string add = Console.ReadLine();



            X509Certificate2 srvCert1 = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN1);
            EndpointAddress address2 = new EndpointAddress(new Uri("net.tcp://"+ add +":"+ port +"/UpdateConfig"), new X509CertificateEndpointIdentity(srvCert1));
            binding2.CloseTimeout = TimeSpan.MaxValue;
            binding2.OpenTimeout = TimeSpan.MaxValue;
            binding2.ReceiveTimeout = TimeSpan.MaxValue;
            binding2.SendTimeout = TimeSpan.MaxValue;

            


            string srvCertCN = "SysLog";
            NetTcpBinding binding1 = new NetTcpBinding();
            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;


            //ELENA 9.11.
            Console.WriteLine("Unesite ipadresu za KanalKaSysLogu:");
            string add1= Console.ReadLine();

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://" + add1 + ":50002/Log"), new X509CertificateEndpointIdentity(srvCert));

            MakeSyslogClient proxy1 = new MakeSyslogClient(binding1, address1);
            Console.WriteLine("Server is going to log " + DateTime.Now.ToString("hh.mm.ss.ffffff"));
            string usrname = System.Security.Principal.WindowsIdentity.GetCurrent().Name;


            binding1.CloseTimeout = TimeSpan.MaxValue;

            binding1.OpenTimeout = TimeSpan.MaxValue;

            binding1.ReceiveTimeout = TimeSpan.MaxValue;

            binding1.SendTimeout = TimeSpan.MaxValue;

            

            MakeSyslogClient proxyLog = new MakeSyslogClient(binding1, address1);
            

            using (MakeRBACClient proxy = new MakeRBACClient(binding2, address2))
            {
                try
                {
                    Console.WriteLine("Server is going to update " + DateTime.Now.ToString("hh.mm.ss.ffffff"));

                    proxy.UpdateConfiguration();
                    proxyLog.Logging(Thread.CurrentPrincipal.Identity.Name);
                    

                }
                catch
                {
                    proxyLog.LoggingFail(Thread.CurrentPrincipal.Identity.Name);
                }

            }

            
        }

        public Dictionary<string, List<string>> GetDictionary()
        {

            Dictionary<string, List<string>> GroupsAndPermissionsDict = new Dictionary<string, List<string>>();

            ResXResourceReader rsxr = new ResXResourceReader("..\\..\\GroupsAndPermisions.resx");
            foreach (DictionaryEntry d in rsxr)
            {

                string name = d.Key.ToString();
                string value = d.Value.ToString();
                string[] split = value.Split(',');
                List<string> listaPermisija = split.ToList();
                GroupsAndPermissionsDict.Add(name, listaPermisija);
            }
            return GroupsAndPermissionsDict;


        }
    }
}
