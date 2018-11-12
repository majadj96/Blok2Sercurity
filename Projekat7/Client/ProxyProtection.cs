using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ProxyProtection : ChannelFactory<IFileService>, IFileService, IDisposable
    {
        IFileService factory;


        public ProxyProtection()
        {
            NetTcpBinding binding = new NetTcpBinding();

            Console.WriteLine("Unesite ipadresu za KanalKaServisu:");
            string add = Console.ReadLine();
            Console.WriteLine("Unesite port za KanalKaServisu:");
            string port = Console.ReadLine();

            string address = "net.tcp://" + add + ":" + port + "/FileService";

            ChannelFactory<IFileService> channelFactory = new ChannelFactory<IFileService>(binding, address);

            Console.WriteLine("Choose 't' for Transport Mode or 'm' for Message Mode..");
            string forSend = Console.ReadLine();

            if (forSend.Equals("t"))
            {
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            }
            else if (forSend.Equals("m"))
            {
                binding.Security.Mode = SecurityMode.Message;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;

            }

            factory = channelFactory.CreateChannel();
        }

        public bool CreateFolder(string foldername)
        {
            try
            {
                return factory.CreateFolder(foldername);
            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to CreateFolder(). {0}", comEx.Message);
                return false;
            }
        }

        public bool CreateFile(string filename)
        {
            try
            {
                return factory.CreateFile(filename);
            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to CreateFolder(). {0}", comEx.Message);
                return false;
            }
        }

        public bool ModifyFolderName(string folderName, string newName)
        {
            try
            {
                return factory.ModifyFolderName(folderName, newName);
            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to ModifyFolderName(). {0}", comEx.Message);
                return false;
            }
        }

        public bool ModifyFile(string FileName, string text)
        {
            try
            {
                return factory.ModifyFile(FileName, text);
            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to ModifyFile(). {0}", comEx.Message);
                return false;
            }
        }

        public string Read(string FileName)
        {
            try
            {
                return factory.Read(FileName);
            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to Read(). {0}", comEx.Message);
                return "";
            }
        }

        public bool DeleteFolder(string folderName)
        {
            try
            {
                return factory.DeleteFolder(folderName);
            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to DeleteFolder(). {0}", comEx.Message);
                return false;
            }
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                return factory.DeleteFile(fileName);
            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("Error while trying to DeleteFile(). {0}", comEx.Message);
                return false;
            }
        }

        public void CheckLevel()
        {
            try
            {
                factory.CheckLevel();
                Console.WriteLine("Successfully conected to the servis :)");

            }
            catch (CommunicationException comEx)
            {
                Console.WriteLine("There is no server with that kind of binding mode eror: " + comEx.Message);
            }
        }
    }
}
