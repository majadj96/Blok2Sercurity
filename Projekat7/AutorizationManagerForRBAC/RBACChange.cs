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


            foreach (string grupa in GroupsAndPermissionsDict.Keys)
            {

                foreach (string permisija in GroupsAndPermissionsDict[grupa])
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
            X509Certificate2 srvCert1 = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN1);

            binding2.CloseTimeout = TimeSpan.MaxValue;
            binding2.OpenTimeout = TimeSpan.MaxValue;
            binding2.ReceiveTimeout = TimeSpan.MaxValue;
            binding2.SendTimeout = TimeSpan.MaxValue;

            foreach (string s in ListOfServers)
            {
                EndpointAddress address2 = new EndpointAddress(new Uri("net.tcp://" + s + "/UpdateConfig"), new X509CertificateEndpointIdentity(srvCert1));

                using (MakeRBACClient proxy = new MakeRBACClient(binding2, address2))
                {

                    try
                    {
                        Console.WriteLine("Server is going to update " + DateTime.Now.ToString("hh.mm.ss.ffffff"));

                        proxy.UpdateConfiguration();

                        Program.proxyLog.Logging(Thread.CurrentPrincipal.Identity.Name);


                    }
                    catch
                    {
                        Program.proxyLog.LoggingFail(Thread.CurrentPrincipal.Identity.Name);
                    }

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
