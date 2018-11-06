using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("cao");


            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/FileService";
            Console.WriteLine("Izaberite mode: t ili m");
            string forSend = Console.ReadLine();

            if (forSend.Equals("t")) {
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                Console.WriteLine("TRANSPORT");

            }
            else if(forSend.Equals("m"))
            {
                binding.Security.Mode = SecurityMode.Message;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                Console.WriteLine("MESSAGE");

            }


            using (ProxyProtection proxy = new ProxyProtection(binding, address))
            {
                Console.WriteLine("Unesite ime foldera:");
                string imeFoldera = Console.ReadLine();
                proxy.CreateFolder(imeFoldera);
                //proxy.CreateFile("fajl");
                Console.ReadLine();
            }


        }
    }
}
