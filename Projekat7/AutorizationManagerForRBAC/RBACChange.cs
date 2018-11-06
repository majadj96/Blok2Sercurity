﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutorizationManagerForRBAC
{
    class RBACChange : IRBACChange
    {
        public void Change(Dictionary<string, List<string>> GroupsAndPermissionsDict)
        {
            IResourceWriter writer = new ResourceWriter("..\\..\\..\\Common\\GroupsAndPermisions.resx");

            foreach(string grupa in GroupsAndPermissionsDict.Keys)
            {
                foreach(string permisija in GroupsAndPermissionsDict[grupa])
                {
                    writer.AddResource(grupa, permisija);

                }

            }

            writer.Generate();
            writer.Close();




            Console.WriteLine("Promena u RESX-u!");
            




            string srvCertCN = "Servis";
            NetTcpBinding binding1 = new NetTcpBinding();
            binding1.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            EndpointAddress address1 = new EndpointAddress(new Uri("net.tcp://localhost:50001/UpdateConfig"), new X509CertificateEndpointIdentity(srvCert));

            using (MakeRBACClient proxy = new MakeRBACClient(binding1, address1))
            {
                Console.WriteLine("Kazem serveru da se apdejtuje "+ DateTime.Now.ToString("hh.mm.ss.ffffff"));
                proxy.UpdateConfiguration();

            }







        }

    }
}